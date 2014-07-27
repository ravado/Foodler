using System;
using System.Windows;
using Foodler.Common;
using Foodler.Resources;
using Microsoft.Phone.Tasks;

namespace Foodler.Services
{
    public sealed class VersionFunctionalityService
    {
        public static void ShowUnavailableFunctionalityMessage(string message)
        {
            var result = MessageBox.Show(message, Messages.Common_UnavailableFunctionalityHeader, MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                var webBrowserTask = new WebBrowserTask()
                {
                    Uri = new Uri(Constants.URL_TO_FULL_VERSION)
                };
                webBrowserTask.Show();
            }
        }
    }
}
