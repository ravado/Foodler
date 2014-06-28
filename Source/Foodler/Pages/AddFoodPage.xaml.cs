using System.Linq;
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
        protected AddFoodViewModel ViewModel { get; set; }
        private bool _ignoreFood = false;

        public AddFoodPage()
        {
            InitializeComponent();
            ViewModel = new AddFoodViewModel();
            DataContext = ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Initialize(TransfareManager.SelectedParticipants, TransfareManager.FoodContainer, _ignoreFood);

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _ignoreFood = e.Content is ListPickerPage;
            TransfareManager.FoodContainer = ViewModel.GetFoodContainer();

            base.OnNavigatedFrom(e);
        }


        private void NewJokesMultiSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IParticipant selected;
            if (e.AddedItems.Count > 0)
            {
                selected = e.AddedItems[0] as IParticipant;
                if(selected != null)
                    ViewModel.SelectedParticipants.Add(selected);
            }
            else
            {
                selected = e.RemovedItems[0] as IParticipant;
                ViewModel.SelectedParticipants.Remove(selected);
            }
        }

        #region Callbacks

        private void DoneButtonClick(object sender, EventArgs e)
        {
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