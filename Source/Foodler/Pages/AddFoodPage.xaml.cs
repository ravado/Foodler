using Microsoft.Phone.Controls;
using System.Windows;
using System.Windows.Navigation;

namespace Foodler.Pages
{
    public partial class AddFoodPage : PhoneApplicationPage
    {
        public AddFoodPage()
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