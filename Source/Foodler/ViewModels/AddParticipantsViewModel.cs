using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using Foodler.Common;
using Foodler.Common.Contracts;
﻿using Foodler.Common.Helpers;
﻿using Foodler.Models;
using Foodler.Resources;
using Foodler.Services;
using Foodler.ViewModels.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Foodler.ViewModels
{
    public class AddParticipantsViewModel : BaseViewModel
    {
        #region Fields

        private ICommand _clearAllCommand;
        private ObservableCollection<AlphaKeyGroup<IParticipant>> _avaibleParticipants;
        private bool _isCanceled;
        private bool _addedSelf;
        #endregion

        #region Properties

        public string AvailableParticipantsLabel { get; set; }
        public string SelectedParticipantsLabel { get; set; }
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

        public Action<IParticipant> RemovedParticipant { get; set; }
        #endregion

        public AddParticipantsViewModel(ParticipantService participantService) : this()
        {
            AvaibleParticipants = new ObservableCollection<AlphaKeyGroup<IParticipant>>();
            ChosenParticipants = new ObservableCollection<IParticipant>();
            ParticipantService = participantService;
            ChosenParticipants.CollectionChanged += ChosenParticipantsOnCollectionChanged;
        }

        private void ChosenParticipantsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var p in args.OldItems.OfType<IParticipant>())
                {
                    RemovedParticipant(p);
                }
            }
        }

        #region Public Methods

        public AddParticipantsViewModel()
        {
            InitLabels();
        }

        /// <summary>
        /// Basic initialization
        /// </summary>
        public void Initialize()
        {
            LoadAvaibleParticipants();
            Debug.WriteLine("Loaded participants");
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
            if (!ChosenParticipants.Contains(participant) && participant != null)
                ChosenParticipants.Add(participant);
        }

        /// <summary>
        /// Gets participants which have been chosen as involved
        /// </summary>
        /// <returns>List of involved participants</returns>
        public IEnumerable<Participant> GetInvolvedParticipants()
        {
            if (_isCanceled) return null;

            return ChosenParticipants.Select(p => new Participant(p)).ToList();
        }

        /// <summary>
        /// Set involved into party participants
        /// </summary>
        /// <param name="participants">Involved participants</param>
        public void SetInvolvedParticipants(IEnumerable<IParticipant> participants)
        {
            if (ChosenParticipants != null)
            {
                ChosenParticipants.Clear();
                foreach (var participant in participants)
                {
                    ChosenParticipants.Add(participant);
                }
            }
        }

        /// <summary>
        /// Just removing participant from list of selected ones
        /// </summary>
        /// <param name="participantToRemove">Which participant to remove</param>
        public void RemoveSelectedParticipantFromList(IParticipant participantToRemove)
        {
            if (!_isRemoving)
            {
                _isRemoving = true;
                try
                {
                    ChosenParticipants.Remove(participantToRemove);
                }
                finally
                {
                    _isRemoving = false;
                }
            }

        }

        private bool _isRemoving = false;

        #endregion

        #region Private Methods

        private void ClearAll()
        {
            ChosenParticipants.Clear();
        }

        #region Callbacks
        #endregion

        #endregion


        public void InitLabels()
        {
            AvailableParticipantsLabel = UILabels.AddParticipantsPage_AvailableTabHeader;
            SelectedParticipantsLabel = UILabels.AddParticipantsPage_SelectedTabHeader;
        }

        public void CancelAddingParticipants()
        {
            _isCanceled = true;
        }



        public bool IsValid { get { return Validate(); }}

        private bool Validate()
        {
            if (ChosenParticipants.Count < 2)
            {
                return false;
            }

            return true;
        }

        internal void AddSelf(bool force = false)
        {
            if(_addedSelf && !force) return;

            var me = new Participant(Guid.Empty, UILabels.Common_SelfName, false, null);
            if (!ChosenParticipants.Contains(me))
            {
                ChosenParticipants.Add(me);
                _addedSelf = true;
            }
        }
    }
}
