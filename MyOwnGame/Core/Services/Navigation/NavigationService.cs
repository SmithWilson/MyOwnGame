using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;
using Unity;

namespace MyOwnGame.Core.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private Frame _frame;
        private IUnityContainer _unityContainer;

        public NavigationService(IUnityContainer unityContainer) =>
            _unityContainer = unityContainer ?? throw new ArgumentNullException(nameof(unityContainer));


        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Page View => _frame?.Content as Page;

        /// <summary>
        /// 
        /// </summary>
        public object ViewModel => (_frame?.Content as Page)?.DataContext;
        #endregion


        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="frame"></param>
        public void Build(Frame frame)
        {
            _frame = frame ?? throw new ArgumentNullException(nameof(frame));

            _frame.NavigationService.Navigated += _frame_Navigated;
            _frame.NavigationService.Navigating += _frame_Navigating;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="parameter"></param>
        public void Navigate<TViewModel>(object parameter = default) where TViewModel : INavigableViewModel
        {
            if (_frame == null)
            {
                throw new Exception("Фрейм не установлен");
            }

            try
            {
                var view = ViewResolver(typeof(TViewModel));
                var vm = _unityContainer.Resolve<TViewModel>();

                view.DataContext = vm;

                _frame.Navigate(view, parameter);
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void GoBack()
        {
            if (_frame == null)
            {
                throw new Exception("Фрейм не установлен");
            }

            if (_frame.CanGoBack)
            {
                _frame.GoBack();
            }
        }
        #endregion


        #region Non-public methods
        private Page ViewResolver(Type vmType)
        {
            var viewName = vmType.Name.Replace("Model", string.Empty);
            var viewType = Type.GetType($"MyOwnGame.Views.{viewName}");

            return Activator.CreateInstance(viewType) as Page;
        }

        private async void _frame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (ViewModel is INavigableViewModel vm)
            {
                await vm.OnNavigatingFrom(new NavigatingFromEventArgs(e.Uri, e.NavigationMode, e.ExtraData));
            }
        }

        private async void _frame_Navigated(object sender, NavigationEventArgs e)
        {
            if (ViewModel is INavigableViewModel vm)
            {
                vm.NavigationService = this;
                await vm.OnNavigatedTo(new NavigatedToEventArgs(e.Uri, e.ExtraData));
            }
        }
        #endregion
    }
}
