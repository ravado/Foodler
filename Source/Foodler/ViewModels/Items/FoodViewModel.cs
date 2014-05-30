using Foodler.ViewModels.Common;

namespace Foodler.ViewModels.Items
{
    public class FoodViewModel : BaseViewModel
    {
        private string _name;
        private decimal _cost;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }
        public decimal Cost
        {
            get { return _cost; }
            set
            {
                _cost = value;
                NotifyPropertyChanged();
            }
        }
    }
}
