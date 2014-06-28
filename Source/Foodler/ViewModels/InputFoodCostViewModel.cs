using System;
using System.Threading;
using Foodler.Common;
using Foodler.ViewModels.Common;

namespace Foodler.ViewModels
{
    public class InputFoodCostViewModel : BaseViewModel
    {
        #region Fields

        private decimal _foodPrice;

        private string _currencySymbol;

        #endregion

        #region Properties

        public decimal FoodPrice
        {
            get { return _foodPrice; }
            set
            {
                _foodPrice = value;
                NotifyPropertyChanged();
            }
        }

        protected bool Canceling { get; set; }

        protected decimal InitFoodPrice { get; set; }

        public string CurrencySymbol
        {
            get { return _currencySymbol; }
            set
            {
                _currencySymbol = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        public InputFoodCostViewModel()
        {
            Canceling = false;
        }

        public void Initialize()
        {
            FoodPrice = 0.0m;
            Canceling = false;
            CurrencySymbol = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol;
        }

        public decimal GetFoodPrice()
        {
            if (Canceling)
                return InitFoodPrice;

            return FoodPrice;
        }

        /// <summary>
        /// Cancel all operations with numpad and return to initial value
        /// </summary>
        public void Cancel()
        {
            Canceling = true;
        }

        public void SetStartFoodCost(decimal cost)
        {
            FoodPrice = cost;
            InitFoodPrice = cost;
        }

        #region Private Methods

        //TODO: hack method :(
        public void Increment(string value)
        {
            var str = FoodPrice.ToString("N2");
            str = str.Replace(SettingsManager.DecimalSeparator, String.Empty);
            
            // not more than 10 numbers
            if (str.Length <= 10)
            {
                str += value;
                if (str[0] == '0')
                    str = str.Substring(1);
                str = str.Insert(str.Length - 2, SettingsManager.DecimalSeparator);

                FoodPrice = decimal.Parse(str);
            }
        }

        //TODO: hack method :(
        public void Decrement()
        {
            var str = FoodPrice.ToString("N2");
            str = str.Replace(SettingsManager.DecimalSeparator, String.Empty);
            str = str.Substring(0, str.Length - 1);
            str = str.Insert(str.Length - 2, SettingsManager.DecimalSeparator);

            FoodPrice = decimal.Parse(str);
        }
        #endregion
    }
}
