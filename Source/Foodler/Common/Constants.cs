using Microsoft.Devices;
using Environment = System.Environment;

namespace Foodler.Common
{
    public sealed class Constants
    {
        public static string[] SPLITTER_CHARACTER;
        public static bool IS_LIGHT_VERSION;
        public const int MAX_PARTICIPANTS_AMOUNT = 3;
        public const int MAX_FOOD_AMOUNT = 5;
        public const string URL_TO_FULL_VERSION = "http://www.windowsphone.com/s?appId=367af9cc-9eea-4303-858f-cee01f8ff2ae";
        public const string ADMOB_FULL_SCREEN_KEY = "ca-app-pub-6160800682686731/6222450006";

        static Constants()
        {
            SPLITTER_CHARACTER = new string[] {"\r\n"};
            IS_LIGHT_VERSION = false;
        }

    }
}
