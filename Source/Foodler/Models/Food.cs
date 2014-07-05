using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;
using Foodler.Common.Contracts;

namespace Foodler.Models
{
    public class Food : BaseModel, IFood, IEquatable<Food>
    {
        #region Fields

        private Guid _id;
        private IList<IParticipant> _eaters;
        private string _name;
        private decimal _price;
        private BitmapImage _icon;

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

        public IList<IParticipant> Eaters
        {
            get { return _eaters; }
            set
            {
                _eaters = value;
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
        public BitmapImage Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        public Food()
        {
            Id = Guid.NewGuid();
        }

        public Food(string name, BitmapImage image = null, decimal price = 0m) : this()
        {
            Name = name;
            Price = price;
            Icon = image;
        }

        public override string ToString()
        {
            return String.Format("{0} {1}", Name, Price);
        }

        public void Reset()
        {
            Icon = null;
            Id = Guid.Empty;
            Name = String.Empty;
            Price = 0.0m;
        }

        public bool Equals(Food other)
        {
            if (other != null && other.Name == Name)
                return true;

            return false;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Food);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
