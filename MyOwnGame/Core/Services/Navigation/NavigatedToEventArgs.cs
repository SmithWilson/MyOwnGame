using System;

namespace MyOwnGame.Core.Services.Navigation
{
    public class NavigatedToEventArgs : INavigatedToEventArgs
    {
        public NavigatedToEventArgs(Uri uri, object parameter = default)
        {
            Uri = uri;
            Parameter = parameter; 
        }

        public Uri Uri { get; private set; }

        public object Parameter { get; private set; }
    }
}
