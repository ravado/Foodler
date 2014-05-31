using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Foodler.Common;
using Foodler.Common.Contracts;
using Foodler.ViewModels;
using Foodler.ViewModels.Items;
using Microsoft.Phone.Controls;

namespace Foodler.Pages
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainViewModel ViewModel { get; private set; }

        public MainPage()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
            DataContext = ViewModel;
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // get chosen participants from addparticipants page
            if (StateManager.InvolvedParticipants != null)
                ViewModel.SetInvolvedParticipants(StateManager.InvolvedParticipants);

            if (TransfareManager.FoodContainer != null)
            {
                ViewModel.FoodContainers.Add(TransfareManager.FoodContainer);
            }

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // which page do we go
            if (e.Content is AddParticipantPage)
            {
                StateManager.InvolvedParticipants = ViewModel.GetInvolvedParticipants();
            }
            else if (e.Content is AddFoodPage)
            {
            }

            base.OnNavigatedFrom(e);
        }

        #region Private Methods
        
        #region Callbacks

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

        #endregion

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

        #endregion

        // remove participants from "participants" pivot
        private void RemoveInvolvedParticipantBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var participantVm = button.DataContext as IParticipant;
                ViewModel.RemoveInvolvedParticipantFromList(participantVm);
            }
        }
    }
}