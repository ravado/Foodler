using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Foodler.Common.Contracts;
using Foodler.Models;
using Foodler.Resources;
using Microsoft.Phone.Reactive;

namespace Foodler.ViewModels.Items
{
    public class FoodContainerViewModel:BaseModel, IEquatable<FoodContainerViewModel>
    {
        #region Fields
        private Guid _id;
        private bool _isExpanded;
        private bool _editableModeOn;
        private IFood _food;
        private ObservableCollection<IParticipant> _participants;
        #endregion

        #region Properties
        
        
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                NotifyPropertyChanged();
            }
        }

        public bool EditableModeOn
        {
            get { return _editableModeOn; }
            set
            {
                _editableModeOn = value;
                NotifyPropertyChanged();
            }
        }

        public IFood Food
        {
            get { return _food; }
            set
            {
                _food = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<IParticipant> Participants
        {
            get { return _participants; }
            set
            {
                _participants = value;
                NotifyPropertyChanged();
            }
        }
        
        public int ParticipantCount
        {
            get { return Participants == null ? 0 : Participants.Count(); }
        }

        public Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }


        public string FoodExpanderParticipantsLabel { get; set; }
        public string FoodExpanderPriceLabel { get; set; }
        public string FoodExpanderTotalParticipantsLabel { get; set; }
        #endregion


        #region Constructors

        public FoodContainerViewModel()
        {
            Id = Guid.NewGuid();
            InitLabels();
        }

        public FoodContainerViewModel(Guid id, IFood food, IEnumerable<IParticipant> participants):this()
        {
            Food = food;
            Participants = new ObservableCollection<IParticipant>(participants);
            Id = id;
        }

        #endregion

        private void InitLabels()
        {
            FoodExpanderParticipantsLabel = UILabels.Controls_FoodExpanderParticipants;
            FoodExpanderPriceLabel = UILabels.Controls_FoodExpanderPrice;
            FoodExpanderTotalParticipantsLabel = UILabels.Controls_FoodExpanderTotalParticipants;
        }

        public override string ToString()
        {
            return String.Format("Food: {0}, Total Participants: {1}", Food, Participants.Count);
        }
        #region IEquatable

        public bool Equals(FoodContainerViewModel other)
        {
            if (other != null && other.Id == Id)
                return true;

            return false;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as FoodContainerViewModel);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion
    }
}
