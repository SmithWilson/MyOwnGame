using System.Threading.Tasks;

namespace MyOwnGame.Core.Services.Navigation
{
    public interface INavigableViewModel
    {
        INavigationService NavigationService { get; set; }

        Task OnNavigatedTo(INavigatedToEventArgs e);

        Task OnNavigatingFrom(INavigatingFromEventArgs e); 
    }
}