using System;
using System.Diagnostics;
using System.Windows.Input;

namespace MyOwnGame.Mvvm
{
    public class DelegateCommand : ICommand
    {
        #region Fields
        private Action _action;
        #endregion


        public DelegateCommand(Action action) =>
            _action = action ?? throw new ArgumentNullException(nameof(action));


        #region ICommand implementation
#pragma warning disable 0067 
        public event EventHandler CanExecuteChanged;
#pragma warning restore 0067

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                _action();
            }
            catch (Exception)
            {
                Debugger.Break();
            }
        }
        #endregion
    }
}