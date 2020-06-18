using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BookZen.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertiesChanged(params string[] propertyNames)
        {
            foreach (var propName in propertyNames)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
