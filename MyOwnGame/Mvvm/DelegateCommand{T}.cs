using System;
using System.Diagnostics;
using System.Windows.Input;

namespace MyOwnGame.Mvvm
{
    public class DelegateCommand<T> : ICommand
    {
        #region Fields
        private Action<T> _action;
        #endregion


        public DelegateCommand(Action<T> action) =>
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
                _action((T)parameter);
            }
            catch (Exception)
            {
                Debugger.Break();
            }
        }
        #endregion
    }
}
