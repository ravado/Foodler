using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AppBarUtils;
using Foodler.Common;
using Foodler.Common.Contracts;
using Foodler.Resources;
using Foodler.Services;
using Foodler.ViewModels;
using Foodler.ViewModels.Items;
using GoogleAds;
using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Tasks;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Foodler.Pages
{
    public partial class MainPage : PhoneApplicationPage
    {
        
        public MainViewModel ViewModel { get; private set; }
        protected MainPivotPage PreviousPivotPage { get; set; }

        public MainPage()
        {
            InitializeComponent();
            InitializePage();
        }

        private void InitializePage()
        {
            ViewModel = new MainViewModel();
            DataContext = ViewModel;
            PreviousPivotPage = MainPivotPage.None;
            ViewModel.Initialize();
            SetApplicationBarForPivot();
        }

        private void InitRateAppRequest()
        {
            // each third run ask user to vote for up if user hasnt vote for app yet
            if (CanAskUserRateApp())
            {
                RunRateAppCheck();
            }
        }

        private bool CanAskUserRateApp()
        {
            var result = (
                App.ApplicationSettings.IsRatingSet == false            // set rating
                && App.ApplicationSettings.AppRunCount > 1              // not first run for sure
                && App.ApplicationSettings.AppRunCount % 5 == 0         // each third run
                && App.DeviceInfo.ConnectionType != ConnectionType.None // internet connection available
                && App.ApplicationSettings.CoolDownElapsed());          // dont bother user for a while (month) after he sent a feedback
            
            //MessageBox.Show(String.Format("Rating set: {0}, Count: {1}, 5:{2}, Connection: {3}, Cooldown: {4}",
            //    App.ApplicationSettings.IsRatingSet, App.ApplicationSettings.AppRunCount,
            //    (App.ApplicationSettings.AppRunCount%5 == 0),
            //    App.DeviceInfo.ConnectionType, App.ApplicationSettings.CoolDownElapsed()));

            return result;
        }

        private void RunRateAppCheck()
        {
            var messageBox = new CustomMessageBox()
            {
                Caption = Messages.MainPage_RateHeader,
                Message = Messages.MainPage_DoYouLikeApp,
                LeftButtonContent = UILabels.Common_Yes,
                RightButtonContent = UILabels.Common_No
            };
            messageBox.Dismissed += (s1, e1) =>
            {
                switch (e1.Result)
                {
                    case CustomMessageBoxResult.LeftButton: // yes
                        InitRateRequest();                        
                        break;
                    case CustomMessageBoxResult.RightButton: // no
                        InitEmailRequest();
                        break;
                    default:
                        App.ApplicationSettings.LastCoolDownActivated = DateTime.UtcNow;
                        break;
                }
            };

            messageBox.Show();
        }

        private void InitEmailRequest()
        {
            var messageBox = new CustomMessageBox()
            {
                Caption = Messages.MainPage_FeedbackHeader,
                Message = Messages.MainPage_FeedbackSendEmail,
                LeftButtonContent = UILabels.Common_Yes,
                RightButtonContent = UILabels.Common_No
            };
            messageBox.Dismissed += (s1, e1) =>
            {
                switch (e1.Result)
                {
                    case CustomMessageBoxResult.LeftButton: // yes
                        Mailer.PrepareEmail(AboutViewModel.EMAIL_FOR_FEEDBACK, AboutViewModel.FEEDBACK_SUBJECT);
                        break;
                }
                App.ApplicationSettings.LastCoolDownActivated = DateTime.UtcNow;
            };

            messageBox.Show();
        }

        private void InitRateRequest()
        {
            var messageBox = new CustomMessageBox()
            {
                Caption = Messages.MainPage_RateHeader,
                Message = Messages.MainPage_PleaseRateApp,
                LeftButtonContent = UILabels.Common_Yes,
                RightButtonContent = UILabels.Common_No
            };
            messageBox.Dismissed += (s1, e1) =>
            {
                switch (e1.Result)
                {
                    case CustomMessageBoxResult.LeftButton: // yes
                        //TODO: duplicate
                        App.ApplicationSettings.IsRatingSet = true;
                        var marketplaceReviewTask = new MarketplaceReviewTask();
                        marketplaceReviewTask.Show();
                        break;
                    default:
                        App.ApplicationSettings.LastCoolDownActivated = DateTime.UtcNow;
                        break;
                }
            };

            messageBox.Show();
        }
        #region Navigation
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // in some cases we dont need ability to get to previous page
            if (NavigationContext.QueryString.ContainsKey("clearStack"))
            {
                NavigationService.RemoveBackEntry();
            }

            InitRateAppRequest();

            if (!SettingsManager.TutorialAlreadyShowed)
            {
                NavigationService.Navigate(new Uri(PageManager.TUTORIAL, UriKind.RelativeOrAbsolute));
            }

            // get chosen participants from addparticipants page
            if (StateManager.InvolvedParticipants != null)
                ViewModel.SetInvolvedParticipants(StateManager.InvolvedParticipants);

            if (StateManager.FoodContainer != null)
            {
                ViewModel.AddFoodContainer(StateManager.FoodContainer);
                //ViewModel.FoodContainers.Add(StateManager.FoodContainer);
                StateManager.FoodContainer = null;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            StateManager.InvolvedParticipants = ViewModel.GetInvolvedParticipants();

            if (CanShowAdvert())
            {
                // dont go to next page while ad is displayed, do it after
                App.FullScreenAd.DismissingOverlay += (sender, args) => base.OnNavigatedFrom(e);
                App.FullScreenAd.ShowAd();
                App.IsAddShowed = true;
                return;

            }

            // which page do we go
            if (e.Content is AddParticipantPage)
            {
            }
            else if (e.Content is AddFoodPage)
            {
            }

            base.OnNavigatedFrom(e);
        }

        private bool CanShowAdvert()
        {
           return (!App.IsAddShowed && App.IsAddLoaded && App.ApplicationSettings.AppRunCount > 1);
        }

        #endregion

        #region Private Methods

        #region Callbacks

        internal void BtnAddParticipants_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri(PageManager.ADD_PARTICIPANT, UriKind.RelativeOrAbsolute));
        }

        internal void BtnGoFoodTab_OnClick(object sender, EventArgs e)
        {
            Debug.WriteLine("[{0:hh:mm:ss.fff}] GoFoodTab", DateTime.Now);
            SwitchPivot(1);
        }

        internal void BtnGoSumTab_OnClick(object sender, EventArgs e)
        {
            Debug.WriteLine("[{0:hh:mm:ss.fff}] GoSumTab", DateTime.Now);
            ViewModel.SumUpNew();
            SwitchPivot(2);
        }

        internal void BtnAddFood_OnClick(object sender, EventArgs e)
        {
            if (Constants.IS_LIGHT_VERSION && ViewModel.FoodContainers.Count >= Constants.MAX_FOOD_AMOUNT)
            {
                VersionFunctionalityService.ShowUnavailableFunctionalityMessage(
                    String.Format(Messages.MainPage_FoodTabFoodLimitMessage, Constants.MAX_FOOD_AMOUNT));
            }
            else if (ViewModel.Participants.Count == 0)
            {
                MessageBox.Show(Messages.MainPage_NoParticipantsMessage, Messages.Common_AttentionHeader,
                    MessageBoxButton.OK);
            } else
            {
                NavigationService.Navigate(new Uri(PageManager.ADD_FOOD, UriKind.RelativeOrAbsolute));
            }
        }

        public void MenuOpenAbout_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri(PageManager.ABOUT, UriKind.RelativeOrAbsolute));
        }

        internal void BtnDone_OnClick(object sender, EventArgs e)
        {
            Debug.WriteLine("[{0:hh:mm:ss.fff}] GoParticipantTab", DateTime.Now);
            var mr = MessageBox.Show(Messages.MainPage_SumTabResetMessage, Messages.Common_AttentionHeader, MessageBoxButton.OKCancel);
            if (mr == MessageBoxResult.OK)
            {
                ViewModel.Reset();
                ResetPageData();
                SwitchPivot(0);
            }
        }

        private void ResetPageData()
        {
            StateManager.ResetAllData();
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
            SetApplicationBarForPivot((MainPivotPage)MainPivot.SelectedIndex);
            
            // sum page
            if (MainPivot.SelectedIndex == 2) 
            {
                ViewModel.SumUpNew();
            }
        }

        #endregion

        private void SetApplicationBarForPivot(MainPivotPage pivotPage = MainPivotPage.Participants)
        {
            if (PreviousPivotPage != pivotPage)
            {
                switch (pivotPage)
                {
                    case MainPivotPage.Participants:
                        ApplicationBar = AppBarBuilder.Participants.GetAppBarPivotParticipants(this);
                        break;
                    case MainPivotPage.Food:
                        ApplicationBar = AppBarBuilder.Food.GetAppBarPivotFood(this);
                        break;
                    case MainPivotPage.Sum:
                        ApplicationBar = AppBarBuilder.Sum.GetAppBarPivotSum(this);
                        break;
                }
            }

            PreviousPivotPage = pivotPage;
        }

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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("GO");
        }

        private void ListFood_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        internal void BtnToggleFoodEditMode_OnClick(object sender, EventArgs e)
        {
            _autoExpand = true;
            ViewModel.ChangeFoodState(!ViewModel.IsExpandAllFoodOn);
            if (ViewModel.IsExpandAllFoodOn)
            {
                //AppBarBuilder.Food.ShowCollapseAll();
            }
            //else AppBarBuilder.Food.ShowExpandAll();
        }

        private void ExpanderView_OnExpanded(object sender, RoutedEventArgs e)
        {
            //if (ViewModel.IsAllFoodExpanded)
            //{
            //    ViewModel.IsExpandAllOn = true;
            //}
            //else if (ViewModel.IsAllFoodCollapsed)
            //{
            //    ViewModel.IsExpandAllOn = false;
            //}
        }

        private bool _autoExpand;

        private void ListFood_OnTap(object sender, GestureEventArgs e)
        {
            _autoExpand = false;
        }

        private void EditFood_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                var foodContainer = btn.DataContext as FoodContainerViewModel;
                if (foodContainer != null)
                {
                    foodContainer.Participants = new ObservableCollection<IParticipant>(foodContainer.Food.Eaters);
                    StateManager.FoodContainer = foodContainer;
                }

                NavigationService.Navigate(new Uri(PageManager.ADD_FOOD, UriKind.RelativeOrAbsolute));
            }
            
        }

        private void DeleteFood_OnClick(object sender, RoutedEventArgs e)
        {
            var m = MessageBox.Show("You are about to delete this food with all related data. Are you sure?", "Attention", MessageBoxButton.OKCancel);
            if (m == MessageBoxResult.OK)
            {
                var btn = sender as Button;
                if (btn != null)
                {
                    var fc = btn.DataContext as FoodContainerViewModel;

                    ViewModel.RemoveFood(fc);
                }
            }
        }

        internal void BtnExpandCollapseAll_OnClick(object sender, EventArgs e)
        {
            _autoExpand = true;
            ViewModel.ChangeSumState(!ViewModel.IsExpandAllSummaryOn);
            if (ViewModel.IsExpandAllFoodOn)
            {
                //AppBarBuilder.Food.ShowCollapseAll();
            }
            //else AppBarBuilder.Food.ShowExpandAll();
        }

        internal void OpenTutorial_Onclick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri(PageManager.TUTORIAL, UriKind.RelativeOrAbsolute));
        }
    }
}