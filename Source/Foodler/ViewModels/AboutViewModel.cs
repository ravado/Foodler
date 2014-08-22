using System;
using System.Windows;
using System.Windows.Input;
using Foodler.Common;
using Foodler.Common.Contracts;
using Foodler.Resources;
using Foodler.ViewModels.Common;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace Foodler.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private const string EMAIL_FOR_FEEDBACK = "ivan.cherednychok@gmail.com";
        private const string FEEDBACK_SUBJECT = "Deluga Feedback";

        #region Fields

        private ICommand _writeFeedbackCommand;
        private ICommand _showRateAppCommand;

        private bool _rateControlVisible;

        #endregion

        #region Properties

        #region Labels

        public string AboutTabLabel { get; set; }
        public string VersionLabel { get; set; }
        public string DescriptionLabel { get; set; }
        public string WriteEmailTextLabel { get; set; }
        public string RateAppTextLabel { get; set; }
        public string RateAppBtnLabel { get; set; }
        public string WriteEmailBtnLabel { get; set; }
        public string DontMakeBadVoteTitleLabel { get; set; }
        public string DontMakeBadVoteTextLabel { get; set; }
        public string ThankYouVoteLabel { get; set; }
        public string AboutLogo { get; set; }

        #endregion

        public bool RateControlVisible
        {
            get { return _rateControlVisible; }
            set
            {
                _rateControlVisible = value;
                NotifyPropertyChanged();
            }
        }

        public IApplicationSettings ApplicationSettings { get; protected set; }
        public IApplicationInfo ApplicationInfo { get; protected set; }
        
        #endregion

        #region Commands

        public ICommand WriteFeedbackCommand
        {
            get
            {
                _writeFeedbackCommand = _writeFeedbackCommand ?? new ActionCommand(WriteFeedback);
                return _writeFeedbackCommand;
            }
        }
        
        public ICommand ShowRateAppCommand
        {
            get
            {
                _showRateAppCommand = _showRateAppCommand ?? new ActionCommand(ShowRateApp);
                return _showRateAppCommand;
            }
        }

        #endregion

        public AboutViewModel(IApplicationSettings applicationSettings, IApplicationInfo applicationInfo)
        {
            ApplicationSettings = applicationSettings;
            ApplicationInfo = applicationInfo;
            AboutTabLabel = "about";
            VersionLabel = String.Format("Version {0}", ApplicationInfo.GetAppVersion());
            DescriptionLabel = "    App allows you to share meals price in a restaurant, bar, fast food between people you were with. Simple and clear interface makes it easier and faster than calculating it manually.";
            WriteEmailTextLabel = "    If you have any sagestion, complains or question write me an email :)";
            RateAppTextLabel = "    Please rate my app if you like it, to help other to make a desision";
            RateAppBtnLabel = "Rate App";
            WriteEmailBtnLabel = "Write To Author";
            DontMakeBadVoteTitleLabel = "Info";
            DontMakeBadVoteTextLabel = "If you dont like somethig in the app, just write me an email, and help to make this app better. Do you still wanna rate app?";
            ThankYouVoteLabel = "Thank you for your vote!";

            AboutLogo = Constants.IS_LIGHT_VERSION ? Images.TransparentLogoLite : Images.TransparentLogo;
        }

        public void WriteFeedback()
        {
            var emailComposeTask = new EmailComposeTask {Subject = FEEDBACK_SUBJECT, To = EMAIL_FOR_FEEDBACK};
            emailComposeTask.Show();
        }

        public void RateApp(int value)
        {
            //TODO: read this code carefuly, it should be refactored - view mode here know about the view!
            if (value >= 4)
            {
                var marketplaceReviewTask = new MarketplaceReviewTask();
                marketplaceReviewTask.Show();
            }
            else
            {
                var messageBox = new CustomMessageBox()
                {
                    Caption = DontMakeBadVoteTitleLabel,
                    Message = DontMakeBadVoteTextLabel,
                    LeftButtonContent = UILabels.Common_Yes,
                    RightButtonContent = UILabels.Common_No
                };

                messageBox.Dismissed += (s1, e1) =>
                {
                    switch (e1.Result)
                    {
                        case CustomMessageBoxResult.LeftButton:
                            var marketplaceReviewTask = new MarketplaceReviewTask();
                            marketplaceReviewTask.Show();
                            break;
                        case CustomMessageBoxResult.RightButton:
                            // Do something.
                            break;
                        case CustomMessageBoxResult.None:
                            // Do something.
                            break;
                        default:
                            break;
                    }
                };

                messageBox.Show();

                HideRateApp();
            }
        }

        public void ShowRateApp()
        {
            RateControlVisible = true;
        }

        public void HideRateApp()
        {
            RateControlVisible = false;
        }
    }
}
