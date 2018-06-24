using MyOwnGame.Mvvm;
using System.Windows;
using System.Windows.Input;

namespace MyOwnGame.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        #region Fields
        private ICommand _navigateToGameCommand;
        private ICommand _navigateToEditorCommand;
        private ICommand _exitCommand;
        #endregion

        #region Commands 
        public ICommand NavigateToGameCommand => _navigateToGameCommand ??
            (_navigateToGameCommand = new DelegateCommand(() => NavigationService.Navigate<GameViewModel>()));

        public ICommand NavigateToEditorCommand => _navigateToEditorCommand ??
            (_navigateToEditorCommand = new DelegateCommand(() => NavigationService.Navigate<EditorViewModel>()));

        public ICommand ExitCommand => _exitCommand ??
            (_exitCommand = new DelegateCommand(() => Application.Current.Shutdown()));
        #endregion
    }
}
