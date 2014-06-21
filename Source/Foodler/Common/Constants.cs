using Microsoft.Devices;
using Environment = System.Environment;

namespace Foodler.Common
{
    public sealed class Constants
    {
        public static string[] SPLITTER_CHARACTER;

        static Constants()
        {
            SPLITTER_CHARACTER = new string[] {"\r\n"};
        }
    }
}
