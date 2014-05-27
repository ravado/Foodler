using System;
using System.Windows;
using System.Windows.Navigation;
using Foodler.ViewModels;
using Microsoft.Phone.Controls;

namespace Foodler.Pages
{
    public partial class MainPage : PhoneApplicationPage
    {
        private readonly MainViewModel _viewModel;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

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
            base.OnNavigatedTo(e);
            _viewModel.Initialize();
        }

        private void BtnGoFoodTab_OnClick(object sender, RoutedEventArgs e)
        {
            MainPivot.SelectedIndex = (++MainPivot.SelectedIndex);
        }

        private void BtnAddParticipants_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/AddParticipantPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void BtnAddFood_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/AddFoodPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void BtnGoSumTab_OnClick(object sender, RoutedEventArgs e)
        {
            MainPivot.SelectedIndex = (++MainPivot.SelectedIndex);
        }

        private void BtnDone_OnClick(object sender, RoutedEventArgs e)
        {
            MainPivot.SelectedIndex = 0;
        }
    }
}