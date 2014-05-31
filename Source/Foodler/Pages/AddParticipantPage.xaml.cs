using System;
using Foodler.Common;
using Foodler.Common.Contracts;
using Foodler.DB;
using Foodler.Services;
using Foodler.ViewModels;
using Microsoft.Phone.Controls;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;
using Microsoft.Phone.UserData;

namespace Foodler.Pages
{
    public partial class AddParticipantPage : PhoneApplicationPage
    {
        protected AddParticipantsViewModel ViewModel { get; private set; }
        
        public AddParticipantPage()
        {
            InitializeComponent();
            ViewModel = new AddParticipantsViewModel(new ParticipantService());
            DataContext = ViewModel;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            StateManager.InvolvedParticipants = ViewModel.GetInvolvedParticipants();
            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Initialize();
            ViewModel.SetInvolvedParticipants(StateManager.InvolvedParticipants);

            base.OnNavigatedTo(e);
        }

        #region Callbacks

        private void AcceptChangesBtn_OnClick(object sender, EventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
                TransfareManager.SelectedParticipants = ViewModel.ChosenParticipants.ToList();
            }
        }

        private void ListAvaibleParticipants_OnTap(object sender, GestureEventArgs e)
        {

            var myItem = ((LongListSelector)sender).SelectedItem as IParticipant;
            ViewModel.AddSelectedParticipantToList(myItem);
        }

        private void ListChosenParticipants_OnTap(object sender, GestureEventArgs e)
        {
        }

        private void RemoveSelectedParticipantBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var participantVm = button.DataContext as IParticipant;
                ViewModel.RemoveSelectedParticipantFromList(participantVm);
            }
        }
        
        #endregion
        
    }
}