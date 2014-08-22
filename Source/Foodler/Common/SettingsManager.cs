using System;
using System.IO.IsolatedStorage;
using System.Xml;
using Foodler.Common.Contracts;

namespace Foodler.Common
{
    public class SettingsManager : IApplicationSettings, IApplicationInfo
    {
        private const string FIRST_RUN_KEY = "first_run";
        private const string IS_RATING_SET_KEY = "rating_set";
        private const string APP_RUN_COUNT_KEY = "app run count";
        private const string TUTORIAL_SHOWED_KEY = "tutorial_showed";
        private static readonly IsolatedStorageSettings AppSettings = IsolatedStorageSettings.ApplicationSettings;

        public static string DecimalSeparator
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            }
        }

        public static void IncrementAppRun()
        {
            var run = 0;
            if (AppSettings.Contains(APP_RUN_COUNT_KEY))
            {
                run = (int) AppSettings[APP_RUN_COUNT_KEY];
                run++;
                AppSettings[APP_RUN_COUNT_KEY] = run;
            }
            else
            {
                run++;
                AppSettings.Add(APP_RUN_COUNT_KEY, run);
            }
            AppSettings.Save();
        }

        public int AppRunCount
        {
            get
            {
                if (AppSettings.Contains(APP_RUN_COUNT_KEY))
                {
                    var count = (int)AppSettings[APP_RUN_COUNT_KEY];
                    return count;
                }

                return 0;
            }
        }

        public static bool IsFirstRun
        {
            get
            {
                if (AppSettings.Contains(APP_RUN_COUNT_KEY))
                {
                    var count = (int)AppSettings[APP_RUN_COUNT_KEY];
                    if (count > 0)
                        return false;
                }

                return true;
            }
        }

        public static bool TutorialAlreadyShowed
        {
            get
            {
                if (AppSettings.Contains(TUTORIAL_SHOWED_KEY)
                    && (bool)AppSettings[TUTORIAL_SHOWED_KEY])
                {
                    return true;
                }

                return false;
            }

            set
            {
                if (AppSettings.Contains(TUTORIAL_SHOWED_KEY))
                {
                    AppSettings[TUTORIAL_SHOWED_KEY] = value;
                }
                else
                {
                    AppSettings.Add(TUTORIAL_SHOWED_KEY, value);
                }
                AppSettings.Save();
            }
        }

        /// <summary>
        /// Recalculate the state of the tutorial, using for be sure that users who had installed app before will not see tutorial again
        /// </summary>
        public void RecalculateTutorialStatus()
        {
            if (!TutorialAlreadyShowed)
            {
                // check for those users who had installed the app before tutorial checker was implemented
                // if app had been opened before, probably user already saw the tutorial
                if (AppRunCount > 0)
                {
                    TutorialAlreadyShowed = true;
                }
            }
        }

        public string GetAppVersion()
        {
            var xmlReaderSettings = new XmlReaderSettings
            {
                XmlResolver = new XmlXapResolver()
            };

            using (var xmlReader = XmlReader.Create("WMAppManifest.xml", xmlReaderSettings))
            {
                xmlReader.ReadToDescendant("App");

                return xmlReader.GetAttribute("Version");
            }
        }

        public bool IsRatingSet
        {
            get
            {
                if (AppSettings.Contains(IS_RATING_SET_KEY))
                {
                    return (bool) AppSettings[IS_RATING_SET_KEY];
                }

                return false;
            }
            set
            {
                if (AppSettings.Contains(IS_RATING_SET_KEY))
                {
                    AppSettings[IS_RATING_SET_KEY] = value;
                }
                else
                {
                    AppSettings.Add(IS_RATING_SET_KEY, value);
                }
                AppSettings.Save();
            }
        }
    }
}
