using Foodler.ViewModels.Common;

namespace Foodler.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string AboutTabLabel { get; set; }

        public AboutViewModel()
        {
            AboutTabLabel = "about";
        }
    }
}
