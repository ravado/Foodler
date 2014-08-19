using System;
using System.IO.IsolatedStorage;
using System.Xml;

namespace Foodler.Common
{
    public static class SettingsManager
    {
        private const string FIRST_RUN_KEY = "first_run";
        private const string APP_RUN_COUNT_KEY = "app run count";
        private const string TUTORIAL_SHOWED_KEY = "tutorial_showed";
        private static IsolatedStorageSettings _appSettings = IsolatedStorageSettings.ApplicationSettings;

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
            if (_appSettings.Contains(APP_RUN_COUNT_KEY))
            {
                run = (int) _appSettings[APP_RUN_COUNT_KEY];
                run++;
                _appSettings[APP_RUN_COUNT_KEY] = run;
            }
            else
            {
                run++;
                _appSettings.Add(APP_RUN_COUNT_KEY, run);
            }
            _appSettings.Save();
        }

        public static int AppRunCount
        {
            get
            {
                if (_appSettings.Contains(APP_RUN_COUNT_KEY))
                {
                    var count = (int)_appSettings[APP_RUN_COUNT_KEY];
                    return count;
                }

                return 0;
            }
        }

        public static bool IsFirstRun
        {
            get
            {
                if (_appSettings.Contains(APP_RUN_COUNT_KEY))
                {
                    var count = (int)_appSettings[APP_RUN_COUNT_KEY];
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
                if (_appSettings.Contains(TUTORIAL_SHOWED_KEY)
                    && (bool)_appSettings[TUTORIAL_SHOWED_KEY])
                {
                    return true;
                }

                return false;
            }

            set
            {
                if (_appSettings.Contains(TUTORIAL_SHOWED_KEY))
                {
                    _appSettings[TUTORIAL_SHOWED_KEY] = value;
                }
                else
                {
                    _appSettings.Add(TUTORIAL_SHOWED_KEY, value);
                }
                _appSettings.Save();
            }
        }

        /// <summary>
        /// Recalculate the state of the tutorial, using for be sure that users who had installed app before will not see tutorial again
        /// </summary>
        public static void RecalculateTutorialStatus()
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


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetAppVersion()
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
    }
}
