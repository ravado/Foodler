﻿using Foodler.Common;
using Foodler.Common.Contracts;
using Foodler.Models;
using System.Windows.Input;

namespace Foodler.ViewModels.Items
{
    public class ParticipantViewModel:Participant
    {
        private ICommand _incrementParticipantAteCommand;
        private ICommand _decrementParticipantAteCommand;
        private bool _ateRangeActivated;

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
        public bool AteRangeActivated
        {
            get { return _ateRangeActivated; }
            set
            {
                _ateRangeActivated = value;
                NotifyPropertyChanged();
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
            ParticipantAteCoefficient = participant.ParticipantAteCoefficient;
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
