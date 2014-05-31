using System.Windows.Controls;
using Foodler.Common;
using Foodler.Common.Contracts;
using Foodler.ViewModels;
using Foodler.ViewModels.Items;
using Microsoft.Phone.Controls;
using System.Windows;
using System.Windows.Navigation;

namespace Foodler.Pages
{
    public partial class AddFoodPage : PhoneApplicationPage
    {
        private AddFoodViewModel _viewModel;

        public AddFoodPage()
        {
            InitializeComponent();
            _viewModel = new AddFoodViewModel();
            DataContext = _viewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            _viewModel.Initialize(TransfareManager.SelectedParticipants, null);
        }

        private void BtnDone_OnClick(object sender, RoutedEventArgs e)
        {
            TransfareManager.FoodContainer = _viewModel.GetFoodContainer();
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void NewJokesMultiSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IParticipant selected;
            if (e.AddedItems.Count > 0)
            {
                selected = e.AddedItems[0] as IParticipant;
                if(selected != null)
                    _viewModel.SelectedParticipants.Add(selected);
            }
            else
            {
                selected = e.RemovedItems[0] as IParticipant;
                _viewModel.SelectedParticipants.Remove(selected);
            }
        }
    }
}