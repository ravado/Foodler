using System.Linq;
using System.Windows.Controls;
using System.Windows.Navigation;
using Foodler.Common;
using Foodler.Common.Contracts;
using Foodler.ViewModels;
using Microsoft.Phone.Controls;

namespace Foodler.Pages
{
    public partial class AddAnonymousParticipant : PhoneApplicationPage
    {
        protected AddAnonymousParticipantViewModel ViewModel { get; set; }

        public AddAnonymousParticipant()
        {
            InitializeComponent();
            ViewModel = new AddAnonymousParticipantViewModel();
            DataContext = ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Initialize();
            base.OnNavigatedTo(e);
        }
        
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            StateManager.SelectedAnonymous = ViewModel.GetSelectedAnonymous();
            base.OnNavigatedFrom(e);
        }

        private void AvailableAnonymousList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                foreach (var participant in e.AddedItems.OfType<IParticipant>())
                {
                    ViewModel.SetSelectedAnonymous(participant);
                }

                if (NavigationService.CanGoBack)
                    NavigationService.GoBack();
            }
        }
    }
}