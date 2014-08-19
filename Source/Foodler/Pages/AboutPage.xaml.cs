using System;
using System.Windows;
using Foodler.Common;
using Foodler.ViewModels;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Tasks;

namespace Foodler.Pages
{
    public partial class AboutPage : PhoneApplicationPage
    {
        public AboutViewModel ViewModel { get; private set; }

        public AboutPage()
        {
            InitializeComponent();
            InitializePage();
        }

        private void InitializePage()
        {
            ViewModel = new AboutViewModel();
            DataContext = ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private void WriteEmail_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = "Deluga Feedback";
            //emailComposeTask.Body = "message body";
            emailComposeTask.To = "ivan.cherednychok@gmail.com";

            emailComposeTask.Show();
        }

        private void RateApp_OnClick(object sender, RoutedEventArgs e)
        {
            RatingBar.Visibility = Visibility.Visible;
            RateAppButton.Visibility = Visibility.Collapsed;
        }

        private void RatingBar_OnValueChanged(object sender, EventArgs e)
        {
            var rating = (sender as Rating);

            if (rating == null) return;

            if (rating.Value >= 4)
            {
                var marketplaceReviewTask = new MarketplaceReviewTask();
                marketplaceReviewTask.Show();
            }
            else
            {
                var result = MessageBox.Show(
                    "If you dont like somethig in the app, just write me an email, and help to make this app better. Do you still wanna rate app?",
                    "Please", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    var marketplaceReviewTask = new MarketplaceReviewTask();
                    marketplaceReviewTask.Show();
                }

                RatingBar.Visibility = Visibility.Collapsed;
                RateAppButton.Visibility = Visibility.Visible;
            }
        }
    }
}