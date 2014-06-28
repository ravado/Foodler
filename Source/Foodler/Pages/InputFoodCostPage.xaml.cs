using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Foodler.Common;
using Foodler.ViewModels;
using Microsoft.Phone.Controls;

namespace Foodler.Pages
{
    public partial class InputFoodCostPage : PhoneApplicationPage
    {
        protected InputFoodCostViewModel ViewModel { get; private set; }

        public InputFoodCostPage()
        {
            InitializeComponent();

            ViewModel = new InputFoodCostViewModel();
            DataContext = ViewModel;
        }

        //TODO: check the right way to override this methods and position of base call
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Initialize();
            ViewModel.SetStartFoodCost(StateManager.FoodPrice);
            StateManager.FoodPrice = default(decimal);

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            StateManager.FoodPrice = ViewModel.GetFoodPrice();
            base.OnNavigatedFrom(e);
        }

        private void NumpadNumericBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                try
                {
                    if (btn == NumpadPointBtn)
                    {
                        ViewModel.Increment("00");
                    }
                    else
                    {
                        ViewModel.Increment(btn.Content.ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void NumpadEraseBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // erease last digit here
                ViewModel.Decrement();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NumpadCancelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            // reset value and go to previous page without any changes here
            ViewModel.Cancel();

            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }

        private void NumpadEnterBtn_OnClick(object sender, RoutedEventArgs e)
        {
            // accept inserted value and go back to the previous page
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            ViewModel.Cancel();
            base.OnBackKeyPress(e);
        }
    }
}