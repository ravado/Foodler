﻿using System;
using System.Collections.Specialized;
using System.Linq;
using Foodler.Common.Contracts;
using Foodler.Models;
using Foodler.Resources;
using Foodler.ViewModels.Common;
using Foodler.ViewModels.Items;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Foodler.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Fields

        private IParticipant _selectedParticipant;
        private decimal _foodTotalCost;
        private bool _isExpandAllFoodOn;
        private bool _isExpandAllSummaryOn;
        private bool _isAnyParticipant;
        private bool _isAnyFood;
        private bool _isAnyFoodAndParticipant;
        #endregion

        #region Properties

        public IParticipant SelectedParticipant
        {
            get { return _selectedParticipant; }
            set { _selectedParticipant = value; NotifyPropertyChanged(); }
        }
        public ObservableCollection<IParticipant> Participants { get; set; }
        public ObservableCollection<FoodContainerViewModel> FoodContainers { get; set; }
        public ObservableCollection<ParticipantContainerViewModel> ParticipantContainers { get; set; }
        public decimal FoodTotalCost
        {
            get { return _foodTotalCost; }
            set
            {
                _foodTotalCost = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsExpandAllFoodOn
        {
            get { return _isExpandAllFoodOn; }
            set
            {
                _isExpandAllFoodOn = value;
                if (FoodContainers != null)
                {
                    foreach (var fc in FoodContainers)
                    {
                        fc.IsExpanded = _isExpandAllFoodOn;
                    }
                }
                NotifyPropertyChanged();
            }
        }
        public bool IsExpandAllSummaryOn
        {
            get { return _isExpandAllSummaryOn; }
            set
            {
                _isExpandAllSummaryOn = value;
                if (ParticipantContainers != null)
                {
                    foreach (var pc in ParticipantContainers)
                    {
                        pc.IsExpanded = _isExpandAllSummaryOn;
                    }
                }
                NotifyPropertyChanged();
            }
        }
        public bool IsAnyParticipant
        {
            get { return _isAnyParticipant; }
            set
            {
                _isAnyParticipant = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsAnyFood
        {
            get { return _isAnyFood; }
            set
            {
                _isAnyFood = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsAnyFoodAndParticipant
        {
            get { return _isAnyFoodAndParticipant; }
            set
            {
                _isAnyFoodAndParticipant = value;
                NotifyPropertyChanged();
            }
        }

        #region Labels
        
        public string ParticipantsTabLabel { get; set; }
        public string FoodTabLabel { get; set; }
        public string SummaryTabLabel { get; set; }

        public string NoParticipantsHintLabel { get; set; }
        public string NoFoodHintLabel { get; set; }
        public string NoSummaryHintLabel { get; set; }

        public string TotalSumLabel { get; set; }

        #endregion

        #endregion

        public MainViewModel()
        {
            InitLabels();
            Participants = new ObservableCollection<IParticipant>();
            Participants.CollectionChanged += ParticipantsOnCollectionChanged;
            FoodContainers = new ObservableCollection<FoodContainerViewModel>();
            FoodContainers.CollectionChanged += FoodContainersOnCollectionChanged;
            ParticipantContainers = new ObservableCollection<ParticipantContainerViewModel>();
            ParticipantContainers.CollectionChanged += ParticipantContainersOnCollectionChanged;
        }
 
        #region Private Methods

        private void UpdateTotalCost()
        {
            var cost = 0.0m;
            if (FoodContainers != null)
            {
                cost += FoodContainers.Where(food => food.Food != null).Sum(food => food.Food.Price);
            }
            FoodTotalCost = cost;
        }

        private void RearangeParticipantContainers()
        {
            // magic logic here, do not touch until real nessecery!

            ParticipantContainers.Clear();
            var foods = FoodContainers.Select(f => f.Food).ToArray();
            var eaters = new List<IParticipant>();

            foreach (var e in foods.SelectMany(f => f.Eaters))
            {
                if (!eaters.Contains(e))
                    eaters.Add(e);
            }

            foreach (var eater in eaters)
            {
                foreach (var food in foods)
                {
                    if (food.Eaters.Contains(eater))
                        if (!eater.EatenFood.Contains(food))
                            eater.EatenFood.Add(food);
                }
            }

            foreach (var participant in eaters)
            {
                ParticipantContainers.Add(new ParticipantContainerViewModel(participant, participant.EatenFood));
            }
        }

        #region Callbacks
        private void ParticipantContainersOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            IsAnyFoodAndParticipant = ParticipantContainers.Count > 0;
        }

        private void FoodContainersOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            IsAnyFood = FoodContainers.Count > 0;
        }

        private void ParticipantsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            IsAnyParticipant = Participants.Count > 0;
        }

        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        /// Basic initialization
        /// </summary>
        public void Initialize()
        {
            
        }
        
        public  void InitLabels()
        {
            ParticipantsTabLabel = UILabels.MainPage_ParticipantsTabHeader;
            SummaryTabLabel = UILabels.MainPage_SummaryTabHeader;
            FoodTabLabel = UILabels.MainPage_FoodTabHeader;
            TotalSumLabel = UILabels.MainPage_TotalSum;

            NoFoodHintLabel = UILabels.MainPage_NoFoodHint;
            NoParticipantsHintLabel = UILabels.MainPage_NoParticipantsHint;
            NoSummaryHintLabel = UILabels.MainPage_NoSummaryHint;
        }

        /// <summary>
        /// Calc total sum and sum for every party participant
        /// </summary>
        [Obsolete("Use SumUpNew() instead")]
        public void SumUp()
        {
            SumUpNew(); return;
            var people = new Dictionary<string, decimal>();
            foreach (var fc in FoodContainers)
            {
                var cost = fc.Food.Price;
                var equalPrice = cost / fc.ParticipantCount;
                foreach (var participant in fc.Participants)
                {
                    if (people.ContainsKey(participant.Name))
                    {
                        people[participant.Name] += equalPrice;
                    }
                    else
                    {
                        people[participant.Name] = equalPrice;
                    }

                }
            }
            foreach (var p in people)
            {
                ParticipantContainers.Add(new ParticipantContainerViewModel(new Participant(Guid.Empty, p.Key), null)
                {
                    TotalCost = p.Value
                });
            }

            UpdateTotalCost();
        }

        public void SumUpNew()
        {
            foreach (var pc in ParticipantContainers)
            {
                pc.TotalCost = 0;
                var foods = pc.Participant.EatenFood;
                foreach (var food in foods)
                {
                    var equalPrice = food.Price/food.Eaters.Sum(p => p.ParticipantAteCoefficient);
                    pc.TotalCost += equalPrice * food.Eaters.FirstOrDefault(p => p.Equals(pc.Participant)).ParticipantAteCoefficient;
                }
            }

            UpdateTotalCost();
        }

        /// <summary>
        /// Set involved into party participants
        /// </summary>
        /// <param name="participants">Involved participants</param>
        public void SetInvolvedParticipants(IEnumerable<IParticipant> participants)
        {
            if (Participants != null)
            {
                Participants.Clear();
                foreach (var participant in participants)
                {
                    Participants.Add(participant);
                }
            }
        }

        /// <summary>
        /// Getting participants which have already been chosen
        /// </summary>
        /// <returns>List of chosen participants</returns>
        public IEnumerable<IParticipant> GetInvolvedParticipants()
        {
            if (Participants != null)
                return Participants.Select(p => new Participant(p)).ToList();

            return null;
        }

        /// <summary>
        /// Just removing participant from list of selected ones
        /// </summary>
        /// <param name="participantToRemove">Which participant to remove</param>
        public void RemoveInvolvedParticipantFromList(IParticipant participantToRemove)
        {
            Participants.Remove(participantToRemove);
        }

        public bool ChangeFoodState(bool expandAll = false)
        {
            IsExpandAllFoodOn = expandAll;
            return IsExpandAllFoodOn;
        }

        public void AddFoodContainer(FoodContainerViewModel foodContainer)
        {
            var eaters = foodContainer.Food.Eaters.ToList();
            var food = foodContainer.Food;

            foreach (var e in eaters)
            {
                if (e.EatenFood == null)
                    e.EatenFood = new List<IFood>();

                if (!e.EatenFood.Contains(food))
                    e.EatenFood.Add(food);
            }
            food.Eaters = eaters;

            if (!FoodContainers.Contains(foodContainer))
            {
                FoodContainers.Add(new FoodContainerViewModel(foodContainer.Id, food, eaters));
            }
            else
            {
                var foundContainer = FoodContainers.FirstOrDefault(fc => fc.Equals(foodContainer));
                if (foundContainer != null)
                {
                    foundContainer.Food = food;
                    foodContainer.Participants.Clear();
                    foreach (var eater in eaters)
                    {
                        foodContainer.Participants.Add(eater);
                    }
                }
            }

            RearangeParticipantContainers();
        }

        public bool IsAllFoodExpanded
        {
            get
            {
                return FoodContainers.All(fc => fc.IsExpanded);
            }
        }

        public bool IsAllFoodCollapsed
        {
            get
            {
                return FoodContainers.All(fc => !fc.IsExpanded);
            }
        }

        public void RemoveFood(FoodContainerViewModel fc)
        {
            FoodContainers.Remove(fc);
            var eaten = ParticipantContainers.Where(p => p.Participant.EatenFood.Contains(fc.Food)).ToArray();
            foreach (var e in eaten)
            {
                e.Participant.EatenFood.Remove(fc.Food);
            }
        }

        public void Reset()
        {
            SelectedParticipant = null;
            Participants.Clear();
            FoodContainers.Clear();
            ParticipantContainers.Clear();
            FoodTotalCost = 0;
            IsExpandAllFoodOn = false;
        }

        public bool ChangeSumState(bool expandAll)
        {
            IsExpandAllSummaryOn = expandAll;
            return IsExpandAllSummaryOn;
        }

        #endregion
    }
}
