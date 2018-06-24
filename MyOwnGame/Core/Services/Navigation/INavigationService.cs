using System.Windows.Controls;

namespace MyOwnGame.Core.Services.Navigation
{
    public interface INavigationService
    {
        Page View { get; }

        object ViewModel { get; }

        void Navigate<TViewModel>(object parameter = default) where TViewModel : INavigableViewModel;

        void GoBack();

        void Build(Frame frame); 
    }
}