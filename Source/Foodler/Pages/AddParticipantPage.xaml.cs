using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Foodler.Common;
using Foodler.Common.Contracts;
using Foodler.ViewModels;
using Foodler.ViewModels.Items;
using Microsoft.Phone.Controls;
using System.Windows;
using System.Windows.Navigation;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Foodler.Pages
{
    public partial class AddParticipantPage : PhoneApplicationPage
    {
        private readonly AddParticipantsViewModel _viewModel;
        
        public AddParticipantPage()
        {
            InitializeComponent();
            _viewModel = new AddParticipantsViewModel();
            DataContext = _viewModel;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            StateManager.InvolvedParticipants = _viewModel.GetInvolvedParticipants();
            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _viewModel.Initialize();
            _viewModel.SetInvolvedParticipants(StateManager.InvolvedParticipants);

            base.OnNavigatedTo(e);
        }

        #region Callbacks

        private void BtnDone_OnClick(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
                TransfareManager.SelectedParticipants = _viewModel.ChosenParticipants.ToList();
            }
        }

        private void ListAvaibleParticipants_OnTap(object sender, GestureEventArgs e)
        {

            var myItem = ((LongListSelector)sender).SelectedItem as IParticipant;
            _viewModel.AddSelectedParticipantToList(myItem);
        }

        private void ListChosenParticipants_OnTap(object sender, GestureEventArgs e)
        {
        }

        #endregion
    }
}