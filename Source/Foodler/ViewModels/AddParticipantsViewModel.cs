using Foodler.Common;
using Foodler.Common.Contracts;
using Foodler.Common.Helpers;
using Foodler.Models;
using Foodler.Services;
using Foodler.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Foodler.ViewModels
{
    public class AddParticipantsViewModel : BaseViewModel
    {
        #region Fields

        private ICommand _clearAllCommand;
        private ObservableCollection<AlphaKeyGroup<IParticipant>> _avaibleParticipants;

        #endregion

        #region Properties

        public ObservableCollection<AlphaKeyGroup<IParticipant>> AvaibleParticipants
        {
            get { return _avaibleParticipants; }
            set
            {
                _avaibleParticipants = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<IParticipant> ChosenParticipants { get; set; }
        public ParticipantService ParticipantService { get; private set; }
        public ICommand ClearAllCommand
        {
            get
            {
                _clearAllCommand = _clearAllCommand ?? new ActionCommand(ClearAll);
                return _clearAllCommand;
            }
        }

        #endregion

        public AddParticipantsViewModel(ParticipantService participantService)
        {
            AvaibleParticipants = new ObservableCollection<AlphaKeyGroup<IParticipant>>();
            ChosenParticipants = new ObservableCollection<IParticipant>();
            ParticipantService = participantService;
        }

        #region Public Methods

        /// <summary>
        /// Basic initialization
        /// </summary>
        public void Initialize()
        {
            LoadAvaibleParticipants();
        }

        /// <summary>
        /// Load all avaible participants from database
        /// </summary>
        public void LoadAvaibleParticipants()
        {
            var participants = ParticipantService.GetAllParticipants().ToArray();

            if (AvaibleParticipants == null)
                AvaibleParticipants = new ObservableCollection<AlphaKeyGroup<IParticipant>>();
            
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            
            AvaibleParticipants.Clear();
            AvaibleParticipants =
                AlphaKeyGroup<IParticipant>.CreateGroups(participants, culture, (item) => item.Name, true);

        }

        /// <summary>
        /// Add particular participant to list of selected ones
        /// </summary>
        /// <param name="participant">Participant to add</param>
        public void AddSelectedParticipantToList(IParticipant participant)
        {
            ChosenParticipants.Add(participant);
        }

        /// <summary>
        /// Gets participants which have been chosen as involved
        /// </summary>
        /// <returns>List of involved participants</returns>
        public IEnumerable<Participant> GetInvolvedParticipants()
        {
            return ChosenParticipants.Select(p => new Participant(Guid.Empty, p.Name)).ToList();
        }

        /// <summary>
        /// Set involved into party participants
        /// </summary>
        /// <param name="participants">Involved participants</param>
        public void SetInvolvedParticipants(IEnumerable<Participant> participants)
        {
            if (ChosenParticipants != null)
            {
                ChosenParticipants.Clear();
                foreach (var participant in participants)
                {
                    ChosenParticipants.Add(new Participant(Guid.Empty, participant.Name));
                }
            }
        }

        /// <summary>
        /// Just removing participant from list of selected ones
        /// </summary>
        /// <param name="participantToRemove">Which participant to remove</param>
        public void RemoveSelectedParticipantFromList(IParticipant participantToRemove)
        {
            ChosenParticipants.Remove(participantToRemove);
        }

        #endregion

        #region Private Methods

        private void ClearAll()
        {
            ChosenParticipants.Clear();
        }

        #region Callbacks
        #endregion

        #endregion

        
    }
}
