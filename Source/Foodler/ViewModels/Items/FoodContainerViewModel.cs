using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Foodler.Common.Contracts;
using Foodler.Models;
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

        //public bool IsEditing { get; set; }
        #endregion


        #region Constructors

        public FoodContainerViewModel()
        {
            Id = Guid.NewGuid();
        }

        public FoodContainerViewModel(Guid id, IFood food, IEnumerable<IParticipant> participants)
        {
            Food = food;
            Participants = new ObservableCollection<IParticipant>(participants);
            Id = id;
        }

        #endregion

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
