using MyOwnGame.Core.Services.Navigation;
using MyOwnGame.Mvvm;
using System.Threading.Tasks;

namespace MyOwnGame.ViewModels
{
    public abstract class BaseViewModel : Bindable, INavigableViewModel
    {
        public INavigationService NavigationService { get; set; }

        public virtual Task OnNavigatedTo(INavigatedToEventArgs e)
        {
            return Task.FromResult(e);
        }

        public virtual Task OnNavigatingFrom(INavigatingFromEventArgs e)
        {
            return Task.FromResult(e); 
        }
    }
}