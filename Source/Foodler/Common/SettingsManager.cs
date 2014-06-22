namespace Foodler.Common
{
    public static class SettingsManager
    {
        public static string DecimalSeparator
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            }
        }
    }
}
