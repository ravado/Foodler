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
        /// Indicates how much times application has been launched
        /// </summary>
        int AppRunCount { get; }
    }
}
