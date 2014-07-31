using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Foodler.Common;
using Foodler.ViewModels;
using Foodler.ViewModels.Items;
using Microsoft.Phone.Controls;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Foodler.Pages
{
    public partial class InputFoodCostPage : PhoneApplicationPage
    {
        private bool _isEraseButtonHolded;
        protected InputFoodCostViewModel ViewModel { get; private set; }


        public InputFoodCostPage()
        {
            InitializeComponent();

            ViewModel = new InputFoodCostViewModel();
            DataContext = ViewModel;
            NumpadEraseBtn.ManipulationCompleted += (o, args) => _isEraseButtonHolded = false;
        }

        //TODO: check the right way to override this methods and position of base call
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Initialize();
            var foodContainer = StateManager.FoodContainer;
            if (foodContainer != null && foodContainer.Food != null)
            {
                ViewModel.SetStartFoodCost(foodContainer.Food.Price);
            }

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //StateManager.FoodPrice = ViewModel.GetFoodPrice();
            var foodContainer = StateManager.FoodContainer ?? new FoodContainerViewModel();
            foodContainer.Food.Price = ViewModel.GetFoodPrice();

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
            Debug.WriteLine("Clicked");
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            ViewModel.Cancel();
            base.OnBackKeyPress(e);
        }

        private void NumpadEraseBtn_OnHold(object sender, GestureEventArgs e)
        {
            _isEraseButtonHolded = true;
            try
            {
                // long erasing -> erase symbol each 150 ms until pressed is over
                Task.Run(() =>
                {
                    while (_isEraseButtonHolded)
                    {
                        Dispatcher.BeginInvoke(() => ViewModel.Decrement());
                        Thread.Sleep(150);
                        Debug.WriteLine("Still holded");
                    }    
                }).ContinueWith(r => Debug.WriteLine("Hold event over, status: {0}", r.Status));
                
            }
            catch (Exception ex)
            {
                _isEraseButtonHolded = false;
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}