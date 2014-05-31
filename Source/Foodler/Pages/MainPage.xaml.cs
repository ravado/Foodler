using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Foodler.Common;
using Foodler.ViewModels;
using Microsoft.Phone.Controls;

namespace Foodler.Pages
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainViewModel ViewModel { get; private set; }

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
            DataContext = ViewModel;
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        public string Waiting { get; set; }
        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // get chosen participants from addparticipants page
            if(StateManager.InvolvedParticipants != null)
                ViewModel.SetInvolvedParticipants(StateManager.InvolvedParticipants);
            
            if (TransfareManager.FoodContainer != null)
            {
                ViewModel.FoodContainers.Add(TransfareManager.FoodContainer);
            }

            base.OnNavigatedTo(e);
        }

        private void BtnAddParticipants_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/AddParticipantPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void BtnAddFood_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/AddFoodPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void BtnGoFoodTab_OnClick(object sender, RoutedEventArgs e)
        {
            SwitchPivot(1);
        }

        private void BtnGoSumTab_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.SumUp();
            SwitchPivot(2);
        }

        private void BtnDone_OnClick(object sender, RoutedEventArgs e)
        {
            SwitchPivot(0);
        }

        private async void SwitchPivot(int newIndex)
        {
            UnlockMainPivot();
            await Task.Delay(TimeSpan.FromMilliseconds(50));
            MainPivot.SelectedIndex = newIndex;
            LockMainPivot();
        }
        private void LockMainPivot()
        {
            if (!MainPivot.IsLocked) MainPivot.IsLocked = true;
        }

        private void UnlockMainPivot()
        {
            if (MainPivot.IsLocked) MainPivot.IsLocked = false;
        }
    }
}