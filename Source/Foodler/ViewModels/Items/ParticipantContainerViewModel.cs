using Foodler.Common.Contracts;
using Foodler.Resources;
using Foodler.ViewModels.Common;
using System.Collections.Generic;
using System.Linq;

namespace Foodler.ViewModels.Items
{
    public class ParticipantContainerViewModel:BaseViewModel
    {
        private bool _isExpanded;
        private decimal _totalCost;
        public IParticipant Participant { get; set; }
        public IEnumerable<IFood> Food { get; set; }
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                NotifyPropertyChanged();
            }
        }
        public int FoodCount
        {
            get { return Food == null ? 0 : Food.Count(); }
        }

        public decimal TotalCost
        {
            get { return _totalCost; }
            set
            {
                _totalCost = value;
                NotifyPropertyChanged();
            }
        }

        public string ParticipantsExpanderTotalEatenLabel { get; set; }

        public ParticipantContainerViewModel()
        {
            InitLabels();
        }

        public ParticipantContainerViewModel(IParticipant participant, IEnumerable<IFood> food):this()
        {
            Participant = participant;
            Food = food;
        }

        public void InitLabels()
        {
            ParticipantsExpanderTotalEatenLabel = UILabels.Controls_ParticipantsExpanderTotalEaten;
        }
    }
}
