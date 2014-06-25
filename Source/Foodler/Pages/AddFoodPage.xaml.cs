using System.Windows.Input;
using Foodler.Common;
using Foodler.Common.Contracts;
using Foodler.ViewModels;
using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Foodler.Pages
{
    public partial class AddFoodPage : PhoneApplicationPage
    {
        private AddFoodViewModel _viewModel;

        public AddFoodPage()
        {
            InitializeComponent();
            _viewModel = new AddFoodViewModel();
            DataContext = _viewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            

            _viewModel.Initialize(TransfareManager.SelectedParticipants, null, StateManager.FoodPrice);
            StateManager.FoodPrice = default(decimal);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // which page do we go
            if (e.Content is InputFoodCostPage)
            {
                StateManager.FoodPrice = _viewModel.Food.Price;
            }
            
            base.OnNavigatedFrom(e);
        }


        private void NewJokesMultiSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IParticipant selected;
            if (e.AddedItems.Count > 0)
            {
                selected = e.AddedItems[0] as IParticipant;
                if(selected != null)
                    _viewModel.SelectedParticipants.Add(selected);
            }
            else
            {
                selected = e.RemovedItems[0] as IParticipant;
                _viewModel.SelectedParticipants.Remove(selected);
            }
        }

        #region Callbacks

        private void DoneButtonClick(object sender, EventArgs e)
        {
            TransfareManager.FoodContainer = _viewModel.GetFoodContainer();
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
        


        private void TextFoodCost_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/InputFoodCostPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void TextFoodName_OnClick(object sender, RoutedEventArgs e)
        {
            FoodPicker.Open();
        }

        #endregion
    }
}