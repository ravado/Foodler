using System;

namespace Foodler.Common.Contracts
{
    public interface IApplicationSettings
    {
        /// <summary>
        /// Show if the user has already voted for the app
        /// </summary>
        bool IsRatingSet { get; set; }

        /// <summary>
        /// When user refuse voting or email sending last time
        /// </summary>
        DateTime LastCoolDownActivated { get; set; }

        /// <summary>
        /// For how long we shouldnt bother user after voting or email sending
        /// </summary>
        TimeSpan CoolDownInterval { get; set; }

        /// <summary>
        /// Indicates if we`ve waited enough and can ask user again to vote or write a feedback
        /// </summary>
        /// <returns></returns>
        bool CoolDownElapsed();

        /// <summary>
        /// Indicates how much times application has been launched
        /// </summary>
        int AppRunCount { get; }


        /// <summary>
        /// Date when app was installed for the first time
        /// return DateTime.MinValue if date wasn set yet
        /// </summary>
        DateTime AppInstalledDate { get; set; }


    }
}
