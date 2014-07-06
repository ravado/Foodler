using System.ComponentModel;
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
    public partial class AddFoodPage
    {
        protected AddFoodViewModel ViewModel { get; set; }
        private bool _ignoreFood;
        private bool _updatedSelectedParticipants; // for update only once

        public AddFoodPage()
        {
            InitializeComponent();
            ViewModel = new AddFoodViewModel();
            DataContext = ViewModel;
        }

        #region Navigation

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ResetPageData();
            ViewModel.Initialize(StateManager.InvolvedParticipants, StateManager.FoodContainer, _ignoreFood);
            MarkSelectedParticipants();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _ignoreFood = e.Content is ListPickerPage;
            StateManager.FoodContainer = ViewModel.GetFoodContainer();

            base.OnNavigatedFrom(e);
        }

        #endregion

        #region Private Methods

        private void ResetPageData()
        {
            _updatedSelectedParticipants = false;
            NewJokesMultiSelector.SelectedItems.Clear();
            
        }

        /// <summary>
        /// Mark all selected participants as selected, if they were selected previously
        /// </summary>
        private void MarkSelectedParticipants()
        {
            NewJokesMultiSelector.ItemRealized += (sender, args) =>
            {
                var participants = ViewModel.SelectedParticipants;
                if (!_updatedSelectedParticipants)
                {
                    foreach (var participant in participants)
                    {
                        NewJokesMultiSelector.SelectedItems.Add(participant);
                    }
                    _updatedSelectedParticipants = true;
                }
            };
        }

        #region Callbacks

        private void DoneButtonClick(object sender, EventArgs e)
        {
            if (ViewModel.IsValid)
            {
                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
            }
            else
            {
                MessageBox.Show("You should enter a valid price, and select at least one participant.", "Invalid Data", MessageBoxButton.OK);
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

        private void NewJokesMultiSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                foreach (var added in e.AddedItems)
                {
                    var p = added as IParticipant;
                    if (p != null)
                    {
                        ViewModel.AddSelectedParticipantToList(p);
                    }

                    if (ViewModel.SelectedParticipants.Count > 0)
                    {
                        ActivateAteRangeAppBarButtons();
                    }
                }

                foreach (var removed in e.RemovedItems)
                {
                    var p = removed as IParticipant;
                    if (p != null)
                    {
                        ViewModel.RemoveSelectedParticipantFromList(p);
                    }

                    if (ViewModel.SelectedParticipants.Count == 0)
                    {
                        DeactivateAteRangeAppBarButtons();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeactivateAteRangeAppBarButtons()
        {
            var btn = GetAteRangeAppBarButton();
            if (btn != null)
            {
                //btn.IsEnabled = false;
            }
        }

        private void ActivateAteRangeAppBarButtons()
        {
            var btn = GetAteRangeAppBarButton();
            if (btn != null)
            {
                //btn.IsEnabled = true;
            }
        }

        // get "ate range" button from appbar
        private Microsoft.Phone.Shell.ApplicationBarIconButton GetAteRangeAppBarButton()
        {
            var btn = ApplicationBar.Buttons[1] as Microsoft.Phone.Shell.ApplicationBarIconButton;
            return btn;
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            ViewModel.CancelAddingFood();
            base.OnBackKeyPress(e);
        }

        #endregion

        #endregion

        private void AppBarResetAll_OnClick(object sender, EventArgs e)
        {
            ViewModel.Reset();
            ResetPageData();
        }

        private void AppBarAddAteRange_OnClick(object sender, EventArgs e)
        {
            var btn = GetAteRangeAppBarButton();
            if (ViewModel.AteRangeActivated)
            {
                ViewModel.DeactivateAteRange();
                if (btn != null)
                {
                    btn.IconUri = new Uri("/Assets/AppBar/user-add.png", UriKind.RelativeOrAbsolute);   
                }
            }
            else
            {
                ViewModel.ActivateAteRange();
                if (btn != null)
                {
                    btn.IconUri = new Uri("/Assets/AppBar/user-minus.png", UriKind.RelativeOrAbsolute);
                }
            }
        }

        private void AppBarSelectAll_OnClick(object sender, EventArgs e)
        {
            NewJokesMultiSelector.SelectedItems.Clear();
            foreach (var p in ViewModel.Participants)
            {
                ViewModel.AddSelectedParticipantToList(p);
                NewJokesMultiSelector.SelectedItems.Add(p);
            }
        }
    }
}