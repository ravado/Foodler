using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Foodler.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Foodler.Pages
{
    public partial class TutorialPage : PhoneApplicationPage
    {
        public TutorialViewModel ViewModel { get; private set; }

        public TutorialPage()
        {
            InitializeComponent(); 
            InitializePage();
        }

        private void InitializePage()
        {
            ViewModel = new TutorialViewModel();
            DataContext = ViewModel;
            ViewModel.TutorialFinished += TutorialFinished;
        }

        private void TutorialFinished()
        {
            if(NavigationService.CanGoBack)
                NavigationService.GoBack();
        }

        private void TutGrid_OnTap(object sender, GestureEventArgs e)
        {
            ViewModel.NextTutorial();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            ViewModel.PrevTutorial();
            e.Cancel = true;
        }
    }
}