using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodler.Common.Contracts
{
    public interface IApplicationInfo
    {
        /// <summary>
        /// Gets current app version
        /// </summary>
        /// <returns>Version in format #.#.#.#</returns>
        string GetAppVersion();
    }
}
