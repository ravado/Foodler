﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodler.Common.Contracts
{
    public interface IDeviceInfo
    {
        ConnectionType ConnectionType { get; }
        event Action ConnectionChanged;
    }
}
