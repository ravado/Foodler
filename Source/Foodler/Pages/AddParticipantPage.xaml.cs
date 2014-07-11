using System.ComponentModel;
using Foodler.Common;
using Foodler.Common.Contracts;
using Foodler.Services;
using Foodler.ViewModels;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Foodler.Pages
{
    public partial class AddParticipantPage : PhoneApplicationPage
    {
        protected AddParticipantsViewModel ViewModel { get; private set; }
        private bool _updatedAvailableParticipants = false;
        public AddParticipantPage()
        {
            InitializeComponent();
            ViewModel = new AddParticipantsViewModel(new ParticipantService());
            DataContext = ViewModel;
            ViewModel.RemovedParticipant += RemovedParticipant;
            ApplicationBar = AppBarBuilder.AddParticipants.GetAppBar(this);
        }

        private void RemovedParticipant(IParticipant participant)
        {
            ListAvaibleParticipants.SelectedItems.Remove(participant);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            StateManager.InvolvedParticipants = ViewModel.GetInvolvedParticipants();
            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ResetPageData();
            ViewModel.Initialize();
            
            ViewModel.SetInvolvedParticipants(StateManager.InvolvedParticipants);
            MarkSelectedAvailableParticipants();

            // anonymous can be passed from anonymous page
            ViewModel.AddSelectedParticipantToList(StateManager.SelectedAnonymous);
            StateManager.SelectedAnonymous = null;

            

            base.OnNavigatedTo(e);
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            ViewModel.CancelAddingParticipants();
            base.OnBackKeyPress(e);
        }

        #region Private Methods

        private void ResetPageData()
        {
            _updatedAvailableParticipants = false;
        }

        private void UnmarkAllAvaiableParticipants()
        {
            ListAvaibleParticipants.SelectedItems.Clear();
        }

        #endregion

        /// <summary>
        /// Mark all available participants as selected, if they were selected previously
        /// </summary>
        private void MarkSelectedAvailableParticipants()
        {
            ListAvaibleParticipants.ItemRealized += (sender, args) =>
            {
                var participants = ViewModel.ChosenParticipants;
                if (!_updatedAvailableParticipants)
                {
                    foreach (var participant in participants)
                    {
                        ListAvaibleParticipants.SelectedItems.Add(participant);
                    }
                    _updatedAvailableParticipants = true;
                }
            };
        }

        #region Callbacks

        
       

        private void ListAvaibleParticipants_OnTap(object sender, GestureEventArgs e)
        {

            //var myItem = ((LongListMultiSelector)sender).SelectedItems as IList<IParticipant>;
            //if (myItem != null)
            //{
            //    ViewModel.AddSelectedParticipantToList(myItem);
            //}

        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {

                var button = sender as Button;
                if (button != null)
                {
                    var participantVm = button.DataContext as IParticipant;
                    ViewModel.RemoveSelectedParticipantFromList(participantVm);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RemoveSelectedParticipantBtn_OnTap(object sender, GestureEventArgs e)
        {

        }

        private void RemoveSelectedParticipantBtn_OnClick(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        private void ListAvaibleParticipants_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
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
                }
                foreach (var removed in e.RemovedItems)
                {
                    var p = removed as IParticipant;
                    if (p != null)
                    {
                        ViewModel.RemoveSelectedParticipantFromList(p);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region Application Bar

        internal void AcceptChangesBtn_OnClick(object sender, EventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        internal void AddAnonymousBtn_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/AddAnonymousParticipant.xaml", UriKind.RelativeOrAbsolute));
        }

        internal void ClearAllSelectedBtn_OnClick(object sender, EventArgs e)
        {
            if (ViewModel.ClearAllCommand.CanExecute(null))
            {
                ViewModel.ClearAllCommand.Execute(null);
                UnmarkAllAvaiableParticipants();
            }
        }

        #endregion
    }
}