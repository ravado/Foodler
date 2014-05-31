using Foodler.Common.Contracts;
using Foodler.Models;
using Foodler.ViewModels.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Foodler.ViewModels
{
    public class AddParticipantsViewModel : BaseViewModel
    {
        #region Properties

        public ObservableCollection<IParticipant> AvaibleParticipants { get; set; }
        public ObservableCollection<IParticipant> ChosenParticipants { get; set; }

        #endregion

        public AddParticipantsViewModel()
        {
            AvaibleParticipants = new ObservableCollection<IParticipant>();
            ChosenParticipants = new ObservableCollection<IParticipant>();
        }

        #region Public Methods

        /// <summary>
        /// Basic initialization
        /// </summary>
        public void Initialize()
        {
            var participants = new List<IParticipant>
            {
                new Participant("Ivan"),
                new Participant("Oleg"),
                new Participant("Vadim"),
                new Participant("Julia"),
                new Participant("Eugene"),
                new Participant("Anatoliy"),
                new Participant("Kostya"),
                new Participant("Marina")
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
                    ChosenParticipants.Add(new Participant(participant.Name));
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

        #region Callbacks
        #endregion

        #endregion

        
    }
}
