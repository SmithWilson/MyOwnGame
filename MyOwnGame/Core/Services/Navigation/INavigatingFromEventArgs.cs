using System.Windows.Navigation;

namespace MyOwnGame.Core.Services.Navigation
{
    public interface INavigatingFromEventArgs : INavigatedToEventArgs
    {
        NavigationMode NavigationMode { get; }
    }
}