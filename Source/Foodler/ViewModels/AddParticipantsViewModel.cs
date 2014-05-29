using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foodler.ViewModels.Common;
using Foodler.ViewModels.Items;

namespace Foodler.ViewModels
{
    public class AddParticipantsViewModel : BaseViewModel
    {

        public ObservableCollection<ParticipantViewModel> AvaibleParticipants { get; set; }

        public ObservableCollection<ParticipantViewModel> ChosenParticipants { get; set; }

        public AddParticipantsViewModel()
        {
            AvaibleParticipants = new ObservableCollection<ParticipantViewModel>();
            ChosenParticipants = new ObservableCollection<ParticipantViewModel>();
        }

        public void Initialize()
        {
            var participants = new List<ParticipantViewModel>();
            participants.Add(new ParticipantViewModel("Ivan", OnRemoveParticipant));
            participants.Add(new ParticipantViewModel("Oleg", OnRemoveParticipant));
            participants.Add(new ParticipantViewModel("Vadim", OnRemoveParticipant));
            participants.Add(new ParticipantViewModel("Julia", OnRemoveParticipant));
            participants.Add(new ParticipantViewModel("Eugene", OnRemoveParticipant));
            participants.Add(new ParticipantViewModel("Anatoliy", OnRemoveParticipant));
            participants.Add(new ParticipantViewModel("Kostya", OnRemoveParticipant));
            participants.Add(new ParticipantViewModel("Marina", OnRemoveParticipant));

            if (AvaibleParticipants != null)
            {
                AvaibleParticipants.Clear();
                foreach (var p in participants)
                {
                    AvaibleParticipants.Add(p);
                }
            }
        }
        public void RemoveParticipantFromList(ParticipantViewModel myItem)
        {
            //ChosenParticipants.Remove(myItem);
        }

        private void OnRemoveParticipant(ParticipantViewModel obj)
        {
            ChosenParticipants.Remove(obj);
        }


        internal void AddSelectedParticipantToList(ParticipantViewModel myItem)
        {
            myItem.SubscribeOnDelete(OnRemoveParticipant);
            ChosenParticipants.Add(myItem);
        }
    }
}
