﻿using System;
using Windows.Networking.Connectivity;
using Foodler.Common.Contracts;
using Microsoft.Phone.Net.NetworkInformation;

namespace Foodler.Common
{
    public class DeviceInfo : IDeviceInfo
    {
        public ConnectionType ConnectionType { get; set; }
        public event Action ConnectionChanged;

        public DeviceInfo()
        {
            ConnectionType = ConnectionType.None;
            NetworkInformation.NetworkStatusChanged += NetworkInformationOnNetworkStatusChanged;
        }

        private void NetworkInformationOnNetworkStatusChanged(object sender)
        {
            var result = ConnectionType.None;
            switch (NetworkInterface.NetworkInterfaceType)
            {
                case NetworkInterfaceType.None:
                    result = ConnectionType.None;
                    break;
                case NetworkInterfaceType.Wireless80211:
                    result = ConnectionType.Wifi;
                    break;
                default:
                    result = ConnectionType.Mobile;
                    break;
            }

            ConnectionType = result;
            
            if(ConnectionChanged != null)
                ConnectionChanged();
        }

        
    }
}
