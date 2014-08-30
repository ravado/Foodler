using System;
using System.ComponentModel;

namespace Foodler.Common
{
    [DefaultValue(None)]
    public enum MainPivotPage
    {
        None = -1,
        Participants = 0,
        Food = 1,
        Sum = 2,
    }

    /// <summary>
    /// Represent type of connection to the internet
    /// </summary>
    [Flags]
    [DefaultValue(None)]
    public enum ConnectionType
    {
        None = 0,
        Wifi = 1,
        Mobile = 2
    }
}
