using System;
using System.Windows.Navigation;

namespace MyOwnGame.Core.Services.Navigation
{
    public class NavigatingFromEventArgs : NavigatedToEventArgs, INavigatingFromEventArgs
    {
        public NavigatingFromEventArgs(
            Uri uri, NavigationMode navigationMode, object parameter = default) : base(uri, parameter)
        {
            NavigationMode = NavigationMode; 
        }

        public NavigationMode NavigationMode { get; private set; }
    }
}
