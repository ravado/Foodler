﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using Foodler.Resources;

namespace Foodler.ViewModels.Common
{
    public abstract class BaseViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string AppTitleLabel { get; set; }

        protected BaseViewModel()
        {
            AppTitleLabel = UILabels.Common_AppName;
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
