using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using Foodler.Common;
using Foodler.Common.Contracts;
using Foodler.Common.Helpers;
using Foodler.Models;
using Foodler.Resources;
using Foodler.Services;
using Foodler.ViewModels.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Foodler.ViewModels
{
    public class AddAnonymousParticipantViewModel : BaseViewModel
    {
        #region Fields

        private ObservableCollection<IParticipant> _anonymousMaleParticipantList;
        private ObservableCollection<IParticipant> _anonymousFemaleParticipantList;

        private IParticipant _selectedAnonymous;
        
        private readonly Random _randomizer;
        #endregion

        #region Properties

        public ObservableCollection<IParticipant> AnonymousMaleParticipantList
        {
            get { return _anonymousMaleParticipantList; }
            set
            {
                _anonymousMaleParticipantList = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<IParticipant> AnonymousFemaleParticipantList
        {
            get { return _anonymousFemaleParticipantList; }
            set
            {
                _anonymousFemaleParticipantList = value;
                NotifyPropertyChanged();
            }
        }

        public IParticipant SelectedAnonymous
        {
            get { return _selectedAnonymous; }
            set
            {
                _selectedAnonymous = value;
                NotifyPropertyChanged();
            }
        }

        protected List<string> UrlsToDefaultAvatars { get; set; }

        #endregion

        public AddAnonymousParticipantViewModel()
        {
            _randomizer = new Random();
            UrlsToDefaultAvatars = new List<string>();
            AnonymousMaleParticipantList = new ObservableCollection<IParticipant>();
            AnonymousFemaleParticipantList = new ObservableCollection<IParticipant>();
        }


        #region Public Methods

        /// <summary>
        /// Basic initialization
        /// </summary>
        public void Initialize()
        {
            LoadDefaultAvatarsUrl();
            LoadAvaibleAnonymousParticipants();
        }


        /// <summary>
        /// Load all avaible anonymous participants
        /// </summary>
        public void LoadAvaibleAnonymousParticipants()
        {
            var maleNames = AppResources.MostUsedMaleNames.Split(Constants.SPLITTER_CHARACTER, StringSplitOptions.None);
            foreach (var maleName in maleNames)
            {
                AnonymousMaleParticipantList.Add(new Participant(Guid.NewGuid(), maleName, false, GetRandomAvatar()));
            }

            var femaleNames = AppResources.MostUsedFemaleNames.Split(Constants.SPLITTER_CHARACTER, StringSplitOptions.None);
            foreach (var femaleName in femaleNames)
            {
                AnonymousFemaleParticipantList.Add(new Participant(Guid.NewGuid(), femaleName, false, GetRandomAvatar()));
            }
        }

        public void SetSelectedAnonymous(IParticipant participant)
        {
            SelectedAnonymous = participant;
        }

        public IParticipant GetSelectedAnonymous()
        {
            return SelectedAnonymous;
        }

        #endregion

        #region Private Methods

        private void LoadDefaultAvatarsUrl()
        {
            UrlsToDefaultAvatars = Images.AnimalsNonStroked.Split(Constants.SPLITTER_CHARACTER, StringSplitOptions.None).ToList();
        }

        private byte[] GetRandomAvatar()
        {
            var randIndex = _randomizer.Next(0, UrlsToDefaultAvatars.Count - 1);
            
            var path = UrlsToDefaultAvatars[randIndex];

            return ImageHelper.LoadImageFromPath(path);
        }

        #endregion

        public override void InitLabels()
        {
            throw new NotImplementedException();
        }
    }
}
