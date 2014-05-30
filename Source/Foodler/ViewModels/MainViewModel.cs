using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Foodler.Common;
using Foodler.Models;
using Foodler.ViewModels.Common;
using Foodler.ViewModels.Items;
using Microsoft.Phone.Reactive;

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

        public ObservableCollection<ParticipantViewModel> Participants { get; set; }

        public ObservableCollection<FoodContainerViewModel> FoodContainers { get; set; }

        public MainViewModel()
        {
            Participants = new ObservableCollection<ParticipantViewModel>();
            FoodContainers = new ObservableCollection<FoodContainerViewModel>();
        }

        public void Initialize(List<ParticipantViewModel> participants)
        {
            //var participants = new List<ParticipantViewModel>();
            //participants.Add(new ParticipantViewModel("Ivan", OnRemoveParticipant));
            //participants.Add(new ParticipantViewModel("Oleg", OnRemoveParticipant));
            //participants.Add(new ParticipantViewModel("Vadim", OnRemoveParticipant));
            //participants.Add(new ParticipantViewModel("Julia", OnRemoveParticipant));
            //participants.Add(new ParticipantViewModel("Eugene", OnRemoveParticipant));
            //participants.Add(new ParticipantViewModel("Anatoliy", OnRemoveParticipant));
            //participants.Add(new ParticipantViewModel("Kostya", OnRemoveParticipant));
            //participants.Add(new ParticipantViewModel("Marina", OnRemoveParticipant));

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
    }
}
