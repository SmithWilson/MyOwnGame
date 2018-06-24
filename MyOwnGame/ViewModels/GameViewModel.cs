using MyOwnGame.Core.Services.DataProvider;
using MyOwnGame.Core.Services.Navigation;
using MyOwnGame.Models;
using MyOwnGame.Mvvm;
using NHotkey.Wpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyOwnGame.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        #region Fields
        private IRoundService _roundService;
        private ICommand _exitCommand;
        private TabViewModel _activeTab;
        private List<Round> _rounds;
        private bool _isPreloaderVisible;
        #endregion


        public GameViewModel(IRoundService roundService)
        {
            _roundService = roundService;

            _isPreloaderVisible = true;
            Tabs = new ObservableCollection<TabViewModel>();

            HotkeyManager.Current.AddOrReplace("Create", Key.Right, ModifierKeys.Control, (s, e) =>
            {
                if (Tabs.Count < 3)
                {
#if DEBUG
                    AddRoundDebug();
#else
                    AddRound();
#endif 
                }
            });
        }


        #region Commands 
        public ICommand ExitCommand => _exitCommand ??
            (_exitCommand = new DelegateCommand(() => Application.Current.Shutdown()));
        #endregion


        #region Properties
        public ObservableCollection<TabViewModel> Tabs { get; set; }

        public bool IsPreloaderVisible
        {
            get => _isPreloaderVisible;
            set => Set(ref _isPreloaderVisible, value);
        }

        public TabViewModel ActiveTab
        {
            get => _activeTab;
            set => Set(ref _activeTab, value);
        }
        #endregion


        #region Methods
        public override async Task OnNavigatedTo(INavigatedToEventArgs e)
        {
            _rounds = await _roundService.GetAsync();

#if DEBUG
            AddRoundDebug();
#else
            AddRound();
#endif

            IsPreloaderVisible = false;
        }


        /*
         * TODO: В релизе при добавлении нового раунда может произойти выход за границы массива и вылет приложения. 
         * Добавить обработку этой ошибки и вывод предупреждения о том, что не в БД не хватает какого-то раунда. 
         */
        private void AddRound()
        {
            var round = _rounds[Tabs.Count];
            var categories = round.Topics.Select(topic => new Category
            {
                Topic = topic.Name,
                Items = new ObservableCollection<Question>(topic.Questions)
            });
            var vm = new TabViewModel($"{Tabs.Count + 1} раунд", categories);
            Tabs.Add(vm);
            ActiveTab = vm;
        }

#if DEBUG
        private void AddRoundDebug()
        {
            var categories = new List<Category>();

            for (int i = 0; i < 5; i++)
            {
                var c = new Category
                {
                    Topic = $"Category {i + 1}",
                    Items = new ObservableCollection<Question>()
                };
                for (int j = 0; j < 5; j++)
                {
                    c.Items.Add(new Question
                    {
                        Price = 100 * (j + 1),
                        Text = "Hello, world",
                        Tag = j % 2 == 0 ? QuestionType.Question : QuestionType.Auction, 
                        Answer = "Какой-то длинный и большой ответ"
                    });
                }

                categories.Add(c);
            }

            var vm = new TabViewModel($"{Tabs.Count + 1} раунд", categories);
            Tabs.Add(vm);
            ActiveTab = vm;
        }
#endif
        #endregion
    }
}
 