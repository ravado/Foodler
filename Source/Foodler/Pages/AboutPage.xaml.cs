using Foodler.Common;
using Foodler.ViewModels;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;

namespace Foodler.Pages
{
    public partial class AboutPage : PhoneApplicationPage
    {
        public TutorialViewModel ViewModel { get; private set; }

        public AboutPage()
        {
            InitializePage();
        }

        private void InitializePage()
        {
            ViewModel = new TutorialViewModel();
            DataContext = ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }
    }
}