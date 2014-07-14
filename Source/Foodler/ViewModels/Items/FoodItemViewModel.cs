using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Foodler.Common.Contracts;
using Foodler.Models;
using Foodler.ViewModels.Common;

namespace Foodler.ViewModels.Items
{
    public class FoodItemViewModel : BaseViewModel
    {
        private Guid _id { get; set; }
        private string _name { get; set; }
        private BitmapImage _icon { get; set; }

        public Guid Id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged(); }
        }

        public BitmapImage Icon
        {
            get { return _icon; }
            set { _icon = value; NotifyPropertyChanged(); }
        }

        public FoodItemViewModel()
        {
            Id = Guid.NewGuid();
        }

        public FoodItemViewModel(string name, BitmapImage icon)
            : this()
        {
            Name = name;
            Icon = icon;
        }

        public FoodItemViewModel(Food food)
            : this()
        {
            Id = food.Id;
            Name = food.Name;
            Icon = food.Icon;
        }
    }
}
