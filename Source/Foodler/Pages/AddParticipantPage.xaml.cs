using Microsoft.Phone.Controls;
using System.Windows;
using System.Windows.Navigation;

namespace Foodler.Pages
{
    public partial class AddParticipantPage : PhoneApplicationPage
    {
        public AddParticipantPage()
        {
            InitializeComponent();
        }

        private void BtnDone_OnClick(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}