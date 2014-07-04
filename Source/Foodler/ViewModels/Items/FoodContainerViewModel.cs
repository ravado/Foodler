using System.Collections.Generic;
using System.Linq;
using Foodler.Common.Contracts;
using Foodler.Models;
using Microsoft.Phone.Reactive;

namespace Foodler.ViewModels.Items
{
    public class FoodContainerViewModel:BaseModel
    {
        private bool _isExpanded;
        private bool _editableModeOn;
        
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

        public IFood Food { get; set; }
        public IEnumerable<IParticipant> Participants { get; set; }
        
        public int ParticipantCount
        {
            get { return Participants == null ? 0 : Participants.Count(); }
        }

        //public bool HasNoOptions
        //{
        //    get
        //    {
        //        if(Food == null || Participants)
        //    }
        //}

        public FoodContainerViewModel() {}

        public FoodContainerViewModel(IFood food, IEnumerable<IParticipant> participants)
        {
            Food = food;
            Participants = participants;
        }
    }
}
