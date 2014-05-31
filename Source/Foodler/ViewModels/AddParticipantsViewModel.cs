using Foodler.Models;
using Foodler.ViewModels.Common;
using Foodler.ViewModels.Items;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Foodler.ViewModels
{
    public class AddParticipantsViewModel : BaseViewModel
    {
        #region Properties

        public ObservableCollection<ParticipantViewModel> AvaibleParticipants { get; set; }
        public ObservableCollection<ParticipantViewModel> ChosenParticipants { get; set; }

        #endregion

        public AddParticipantsViewModel()
        {
            AvaibleParticipants = new ObservableCollection<ParticipantViewModel>();
            ChosenParticipants = new ObservableCollection<ParticipantViewModel>();
        }

        #region Public Methods

        /// <summary>
        /// Basic initialization
        /// </summary>
        public void Initialize()
        {
            var participants = new List<ParticipantViewModel>
            {
                new ParticipantViewModel("Ivan", OnRemoveParticipant),
                new ParticipantViewModel("Oleg", OnRemoveParticipant),
                new ParticipantViewModel("Vadim", OnRemoveParticipant),
                new ParticipantViewModel("Julia", OnRemoveParticipant),
                new ParticipantViewModel("Eugene", OnRemoveParticipant),
                new ParticipantViewModel("Anatoliy", OnRemoveParticipant),
                new ParticipantViewModel("Kostya", OnRemoveParticipant),
                new ParticipantViewModel("Marina", OnRemoveParticipant)
            };

            if (AvaibleParticipants == null) return;

            AvaibleParticipants.Clear();
            foreach (var p in participants)
            {
                AvaibleParticipants.Add(p);
            }
        }

        /// <summary>
        /// Add particular participant to list of selected ones
        /// </summary>
        /// <param name="participant">Participant to add</param>
        public void AddSelectedParticipantToList(ParticipantViewModel participant)
        {
            var p = new ParticipantViewModel(participant);
            p.SubscribeOnDelete(OnRemoveParticipant);
            ChosenParticipants.Add(p);
        }

        /// <summary>
        /// Gets participants which have been chosen as involved
        /// </summary>
        /// <returns>List of involved participants</returns>
        public IEnumerable<Participant> GetInvolvedParticipants()
        {
            return ChosenParticipants.Select(p => new Participant(p.Name)).ToList();
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
                    ChosenParticipants.Add(new ParticipantViewModel(participant.Name, OnRemoveParticipant));
                }
            }
        }
        #endregion

        #region Private Methods

        #region Callbacks
        
        private void OnRemoveParticipant(ParticipantViewModel obj)
        {
            var toRemove = ChosenParticipants.LastOrDefault(o => o == obj);
            if (toRemove != null)
                ChosenParticipants.Remove(toRemove);
        }

        #endregion

        #endregion
    }
}
