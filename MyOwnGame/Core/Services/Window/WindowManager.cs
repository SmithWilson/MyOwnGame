using MyOwnGame.Core.Services.Navigation;
using MyOwnGame.ViewModels;
using System;
using System.Windows;

namespace MyOwnGame.Core.Services.Window
{
    public class WindowManager : IWindowManager
    {
        #region Fields
        private readonly INavigationService _navigationService;
        #endregion


        public WindowManager(INavigationService navigationService) =>
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));


        public void Create()
        {
            var app = Application.Current;
            if (app != null)
            {
                var window = new MainWindow();  
                app.MainWindow = window;

                _navigationService.Build(window.RootFrame);
                _navigationService.Navigate<StartViewModel>();

                window.Show(); 
            }
            else
            {
                throw new Exception(); 
            }
        }
    }
}