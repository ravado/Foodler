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
        public const string EMAIL_FOR_FEEDBACK = "ivan.cherednychok@gmail.com";
        public const string FEEDBACK_SUBJECT = "Deluga Feedback";

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
            AboutTabLabel = UILabels.AboutPage_TabAbout;
            VersionLabel = String.Format(UILabels.AboutPage_VersionTitle, ApplicationInfo.GetAppVersion());
            DescriptionLabel = UILabels.AboutPage_Description;
            WriteEmailTextLabel = UILabels.AboutPage_HaveSuggestion;
            RateAppTextLabel = UILabels.AboutPage_RateTheAppText;
            RateAppBtnLabel = UILabels.AboutPage_RateTheAppBtn;
            WriteEmailBtnLabel = UILabels.AboutPage_ConnectWithAuthorBtn;
            
            // messages
            DontMakeBadVoteTitleLabel = Messages.AboutPage_InfoHeader;
            DontMakeBadVoteTextLabel = Messages.AboutPage_DontVoteBadlyText;
            ThankYouVoteLabel = Messages.AboutPage_ThankYouVote;

            AboutLogo = Constants.IS_LIGHT_VERSION ? Images.TransparentLogoLite : Images.TransparentLogo;
        }

        public void WriteFeedback()
        {
            Mailer.PrepareEmail(EMAIL_FOR_FEEDBACK, FEEDBACK_SUBJECT);
        }

        public void RateApp(int value)
        {
            //TODO: read this code carefuly, it should be refactored - view mode here know about the view!
            if (value >= 4)
            {
                //TODO:duplicate

                ApplicationSettings.IsRatingSet = true;
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
                            App.ApplicationSettings.IsRatingSet = true;
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
                    App.ApplicationSettings.LastCoolDownActivated = DateTime.UtcNow;
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
