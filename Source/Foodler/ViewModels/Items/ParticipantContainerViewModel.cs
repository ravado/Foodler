using Foodler.ViewModels.Common;
using System.Collections.Generic;
using System.Linq;

namespace Foodler.ViewModels.Items
{
    public class ParticipantContainerViewModel:BaseViewModel
    {
        private decimal _totalCost;
        public ParticipantViewModel Participant { get; set; }
        public IEnumerable<FoodViewModel> Food { get; set; }

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

        public ParticipantContainerViewModel() {}

        public ParticipantContainerViewModel(ParticipantViewModel participant, IEnumerable<FoodViewModel> food)
        {
            Participant = participant;
            Food = food;
        }
    }
}
