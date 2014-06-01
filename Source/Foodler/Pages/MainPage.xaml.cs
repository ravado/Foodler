using System.Diagnostics;
using Foodler.Common;
using Foodler.Common.Contracts;
using Foodler.ViewModels;
using Microsoft.Phone.Controls;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

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

        internal void BtnAddParticipants_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/AddParticipantPage.xaml", UriKind.RelativeOrAbsolute));
        }

        internal void BtnGoFoodTab_OnClick(object sender, EventArgs e)
        {
            Debug.WriteLine("[{0:hh:mm:ss.fff}] GoFoodTab", DateTime.Now);
            SwitchPivot(1);
        }

        internal void BtnGoSumTab_OnClick(object sender, EventArgs e)
        {
            Debug.WriteLine("[{0:hh:mm:ss.fff}] GoSumTab", DateTime.Now);
            ViewModel.SumUp();
            SwitchPivot(2);
        }

        internal void BtnAddFood_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/AddFoodPage.xaml", UriKind.RelativeOrAbsolute));
        }

        internal void BtnDone_OnClick(object sender, EventArgs e)
        {
            Debug.WriteLine("[{0:hh:mm:ss.fff}] GoParticipantTab", DateTime.Now);
            SwitchPivot(0);
        }

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

        private void MainPivot_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("[{0:hh:mm:ss.fff}] Selection Changed To " + MainPivot.SelectedIndex, DateTime.Now);
            switch (MainPivot.SelectedIndex)
            {
                case 0:
                    ApplicationBar = AppBarBuilder.GetAppBarPivotParticipants(this);
                    break;
                case 1:
                    ApplicationBar = AppBarBuilder.GetAppBarPivotFood(this);
                    break;
                case 2:
                    ApplicationBar = AppBarBuilder.GetAppBarPivotSum(this);
                    break;
            }
        }

        #endregion

        private async void SwitchPivot(int newIndex)
        {
            Debug.WriteLine("[{0:hh:mm:ss.fff}] Switching To " + newIndex, DateTime.Now);

            UnlockMainPivot();
            //await Task.Delay(TimeSpan.FromMilliseconds(50));
            MainPivot.SelectedIndex = newIndex;
            Debug.WriteLine("[{0:hh:mm:ss.fff}] Switched To " + newIndex, DateTime.Now);
            LockMainPivot();
        }

        private async void LockMainPivot()
        {
            Debug.WriteLine("[{0:hh:mm:ss.fff}] Locked", DateTime.Now);
            //MainPivot.IsLocked = true;
        }

        private async void UnlockMainPivot()
        {
            Debug.WriteLine("[{0:hh:mm:ss.fff}] Unlocked", DateTime.Now);
            //MainPivot.IsLocked = false;
        }

        #endregion
       
    }
}