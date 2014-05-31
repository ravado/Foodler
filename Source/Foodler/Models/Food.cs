using System;
using Foodler.Common.Contracts;

namespace Foodler.Models
{
    public class Food : BaseModel, IFood
    {
        #region Fields

        private Guid _id;
        private string _name;
        private decimal _price;

        #endregion

        #region Properties

        public Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }
        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                NotifyPropertyChanged();
            }
        }

        #endregion
    }
}
