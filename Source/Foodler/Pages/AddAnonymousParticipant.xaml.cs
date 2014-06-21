using System.Windows.Navigation;
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

    }
}