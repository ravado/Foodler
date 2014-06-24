using Foodler.Common;
using Foodler.Common.Contracts;
using Foodler.Models;
using System.Windows.Input;

namespace Foodler.ViewModels.Items
{
    public class ParticipantViewModel:Participant
    {
        private ICommand _incrementParticipantAteCommand;
        private ICommand _decrementParticipantAteCommand;

        private int _participantAteCoefficient;

        public int ParticipantAteCoefficient
        {
            get { return _participantAteCoefficient; }
            set
            {
                _participantAteCoefficient = value;
                NotifyPropertyChanged();
            }
        }
        public ICommand IncrementParticipantAteCommand
        {
            get
            {
                _incrementParticipantAteCommand = _incrementParticipantAteCommand ?? new ActionCommand(IncrementParticipantAte);
                return _incrementParticipantAteCommand;
            }
        }

        public ICommand DecrementParticipantAteCommand
        {
            get
            {
                _decrementParticipantAteCommand = _decrementParticipantAteCommand ?? new ActionCommand(DecrementParticipantAte);
                return _decrementParticipantAteCommand;
            }
        }

        public ParticipantViewModel()
        {
            ParticipantAteCoefficient = 1;
        }

        public ParticipantViewModel(IParticipant participant) : this()
        {
            Id = participant.Id;
            Name = participant.Name;
            Avatar = participant.Avatar;
            IsUserContact = participant.IsUserContact;
        }

        private void IncrementParticipantAte()
        {
            if(ParticipantAteCoefficient < 9)
                ParticipantAteCoefficient++;
        }

        private void DecrementParticipantAte()
        {
            if(ParticipantAteCoefficient > 1)
                ParticipantAteCoefficient--;
        }
    }
}
