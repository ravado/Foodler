using System.Linq;
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

        private ParticipantViewModel _selectedParticipant;
        private decimal _foodTotalCost;

        #endregion

        #region Properties

        public ParticipantViewModel SelectedParticipant
        {
            get { return _selectedParticipant; }
            set { _selectedParticipant = value; NotifyPropertyChanged(); }
        }
        public ObservableCollection<ParticipantViewModel> Participants { get; set; }
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
            Participants = new ObservableCollection<ParticipantViewModel>();
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
        
        private void OnRemoveParticipant(ParticipantViewModel participantViewModel)
        {
            var selected = participantViewModel;
            Participants.Remove(selected);
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
                ParticipantContainers.Add(new ParticipantContainerViewModel(new ParticipantViewModel(p.Key, null), null)
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
        public void SetInvolvedParticipants(IEnumerable<Participant> participants)
        {
            if (Participants != null)
            {
                Participants.Clear();
                foreach (var participant in participants)
                {
                    Participants.Add(new ParticipantViewModel(participant.Name, OnRemoveParticipant));
                }
            }
        }

        #endregion
    }
}
