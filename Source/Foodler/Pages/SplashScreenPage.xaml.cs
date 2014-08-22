using System;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Foodler.Common;
using Foodler.Resources;
using Microsoft.Phone.Controls;

namespace Foodler.Pages
{
    //TODO: it`s unuseful now because it makes aplication load much longer
    public partial class SplashScreenPage : PhoneApplicationPage
    {
        public SplashScreenPage()
        {
            InitializeComponent(); 
            InitializePage();
        }

        private void InitializePage()
        {
            if (Constants.IS_LIGHT_VERSION)
                SplashImage.Source = new BitmapImage(new Uri(Images.SplashScreenLite, UriKind.RelativeOrAbsolute));
            else
                SplashImage.Source = new BitmapImage(new Uri(Images.SplashScreen, UriKind.RelativeOrAbsolute));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //NavigationService.Navigate(new Uri(PageManager.MAIN + "?clearStack=true", UriKind.RelativeOrAbsolute));
            //base.OnNavigatedTo(e);
            //Task.Run(() =>
            //{
            //    System.Threading.Thread.Sleep(100);

            //    Dispatcher.BeginInvoke(() =>
            //        NavigationService.Navigate(new Uri(PageManager.MAIN + "?clearStack=true", UriKind.RelativeOrAbsolute)));

            //});
        }
    }
}