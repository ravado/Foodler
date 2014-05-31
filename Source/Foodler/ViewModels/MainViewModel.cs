﻿using System.Linq;
using Foodler.Common.Contracts;
using Foodler.Models;
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

        #endregion

        public MainViewModel()
        {
            Participants = new ObservableCollection<IParticipant>();
            FoodContainers = new ObservableCollection<FoodContainerViewModel>();
            ParticipantContainers = new ObservableCollection<ParticipantContainerViewModel>();
        }

        #region Private Methods

        private void UpdateTotalCost()
        {
            var cost = 0.0m;
            if (FoodContainers != null)
            {
                cost += FoodContainers.Where(food => food.Food != null).Sum(food => food.Food.Cost);
            }
            FoodTotalCost = cost;
        }

        #region Callbacks

        private void OnRemoveParticipant(IParticipant participantViewModel)
        {
            //var selected = participantViewModel;
            //Participants.Remove(selected);
        }

        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        /// Basic initialization
        /// </summary>
        public void Initialize() {}

        /// <summary>
        /// Calc total sum and sum for every party participant
        /// </summary>
        public void SumUp()
        {
            var people = new Dictionary<string, decimal>();
            foreach (var fc in FoodContainers)
            {
                var cost = fc.Food.Cost;
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
                ParticipantContainers.Add(new ParticipantContainerViewModel(new Participant(p.Key), null)
                {
                    TotalCost = p.Value
                });
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
                    Participants.Add(new Participant(participant.Name));
                }
            }
        }

        /// <summary>
        /// Getting participants which have already been chosen
        /// </summary>
        /// <returns>List of chosen participants</returns>
        public IEnumerable<Participant> GetInvolvedParticipants()
        {
            if (Participants != null)
                return Participants.Select(p => new Participant(p.Name)).ToList();

            return null;
        }


        public void RemoveInvolvedParticipantFromList(IParticipant participantToRemove)
        {
            Participants.Remove(participantToRemove);
        }

        #endregion
    }
}
