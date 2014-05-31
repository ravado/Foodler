using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Foodler.Common;
using Foodler.ViewModels.Common;

namespace Foodler.ViewModels.Items
{
    public class ParticipantViewModel:BaseViewModel
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged(); }
        }

        public ICommand RemoveParticipantCommand { get; private set; }

        public Action<ParticipantViewModel> RemoveParticipant;

        public ParticipantViewModel(string name, Action<ParticipantViewModel> removeParticipantHandler)
        {
            Name = name;
            RemoveParticipantCommand = new ActionCommand(OnRemoveParticipant);
            RemoveParticipant += removeParticipantHandler;
        }

        public ParticipantViewModel(ParticipantViewModel participant)
        {
            Name = participant.Name;
            RemoveParticipantCommand = new ActionCommand(OnRemoveParticipant);
        }

        public void SubscribeOnDelete(Action<ParticipantViewModel> handler)
        {
            RemoveParticipant += handler;
        }

        private void OnRemoveParticipant()
        {
            if (RemoveParticipant != null)
                RemoveParticipant(this);
        }
    }
}
