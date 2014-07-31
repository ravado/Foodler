using System;
using System.IO.IsolatedStorage;

namespace Foodler.Common
{
    public static class SettingsManager
    {
        private const string FIRST_RUN_KEY = "first_run";
        private const string APP_RUN_COUNT_KEY = "app run count";
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
    }
}
