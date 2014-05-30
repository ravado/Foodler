using System.Linq;
using Foodler.ViewModels.Common;
using Foodler.ViewModels.Items;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Foodler.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ParticipantViewModel _selectedParticipant;
        public ParticipantViewModel SelectedParticipant
        {
            get { return _selectedParticipant; }
            set { _selectedParticipant = value; NotifyPropertyChanged(); }
        }

        private decimal _foodTotalCost;

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

        public MainViewModel()
        {
            Participants = new ObservableCollection<ParticipantViewModel>();
            FoodContainers = new ObservableCollection<FoodContainerViewModel>();
            ParticipantContainers = new ObservableCollection<ParticipantContainerViewModel>();
        }

        public void Initialize(List<ParticipantViewModel> participants)
        {
            if (Participants != null)
            {
                Participants.Clear();
                foreach (var p in participants)
                {
                    p.SubscribeOnDelete(OnRemoveParticipant);
                    Participants.Add(p);
                }
            }
        }

        private void OnRemoveParticipant(ParticipantViewModel participantViewModel)
        {
            var selected = participantViewModel;
            Participants.Remove(selected);
        }

        private void UpdateTotalCost()
        {
            var cost = 0.0m;
            if (FoodContainers != null)
            {
                cost += FoodContainers.Where(food => food.Food != null).Sum(food => food.Food.Cost);
            }
            FoodTotalCost = cost;
        }
        public void SumUp()
        {
            var people = new Dictionary<string, decimal>();
            foreach (var fc in FoodContainers)
            {
                var cost = fc.Food.Cost;
                var equalPrice = cost/fc.ParticipantCount;
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
    }
}
