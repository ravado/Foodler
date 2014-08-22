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
            //TODO: after ioc dont forget to inject here
            var settings = new SettingsManager();
            ViewModel = new AboutViewModel(settings, settings);
            DataContext = ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }
        
        private void RatingBar_OnValueChanged(object sender, EventArgs e)
        {
            var rating = (sender as Rating);
            if (rating == null) return;
            ViewModel.RateApp((int)rating.Value);
        }
    }
}