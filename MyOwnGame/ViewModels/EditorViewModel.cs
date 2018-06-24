using MyOwnGame.Core.Services.DataProvider;
using MyOwnGame.Core.Services.Navigation;
using MyOwnGame.Models;
using MyOwnGame.Mvvm;
using MyOwnGame.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyOwnGame.ViewModels
{
    public class EditorViewModel : BaseViewModel, IEditorViewModel
    {
        #region Fields
        private IRoundService _roundService;
        private ITopicService _topicService;
        private IQuestionService _questionService;

        private Round _round;
        private Topic _topic;
        private Question _question;
        private bool _roundPopup;
        private bool _topicPopup;
        private bool _questionPopup;
        private string _name;
        public string _text;
        private int _price;
        private bool _isAnswered;
        private bool _isPreloaderVisible;
        private string _answer; 

        private ICommand _addRoundCommand;
        private ICommand _addTopicCommand;
        private ICommand _addQuestionCommand;
        private ICommand _removeRoundCommand;
        private ICommand _removeTopicCommand;
        private ICommand _removeQuestionCommand;
        private ICommand _openRoundPopupCommand;
        private ICommand _openTopicPopupCommand;
        private ICommand _openQuestionPopupCommand;
        private ICommand _closeRoundPopupCommand;
        private ICommand _closeTopicPopupCommand;
        private ICommand _closeQuestionPopupCommand;
        private ICommand _goBackCommand;
        #endregion


        #region Ctors
        public EditorViewModel(
            IRoundService roundService,
            ITopicService topicService,
            IQuestionService questionService)
        {
            _roundService = roundService;
            _topicService = topicService;
            _questionService = questionService;

            Rounds = new ObservableCollection<Round>();
            Topics = new ObservableCollection<Topic>();
            Questions = new ObservableCollection<Question>();

            _name = "Введите название";
            _text = "Введите вопрос";
            _answer = "Введите ответ"; 
            _isPreloaderVisible = true;
        }
        #endregion


        #region Properties
        public ObservableCollection<Round> Rounds { get; set; }

        public ObservableCollection<Topic> Topics { get; set; }

        public ObservableCollection<Question> Questions { get; set; }

        public Round ItemRound
        {
            get => _round;
            set => Set(ref _round, value, UpdateTopic);
        }

        public Topic ItemTopic
        {
            get => _topic;
            set => Set(ref _topic, value, UpdatesQuestion);
        }

        public Question ItemQuestion
        {
            get => _question;
            set => Set(ref _question, value);
        }

        public bool RoundPopup
        {
            get => _roundPopup;
            set => Set(ref _roundPopup, value);
        }

        public bool TopicPopup
        {
            get => _topicPopup;
            set => Set(ref _topicPopup, value);
        }

        public bool QuestionPopup
        {
            get => _questionPopup;
            set => Set(ref _questionPopup, value);
        }

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public string Text
        {
            get => _text;
            set => Set(ref _text, value);
        }

        public int Price
        {
            get => _price;
            set => Set(ref _price, value);
        }

        public bool IsAnswered
        {
            get => _isAnswered;
            set => Set(ref _isAnswered, value);
        }

        public bool IsPreloaderVisible
        {
            get => _isPreloaderVisible;
            set => Set(ref _isPreloaderVisible, value);
        }

        public string Answer
        {
            get => _answer;
            set => Set(ref _answer, value); 
        }

        public string Value { get; set; }

        public QuestionType QuestionType { get; set; }
        #endregion


        #region Commands
        public ICommand AddRoundCommand => _addRoundCommand ??
            (_addRoundCommand = new DelegateCommand(async () => await AddRound()));

        public ICommand AddTopicCommand => _addTopicCommand ??
            (_addTopicCommand = new DelegateCommand(async () => await AddTopic()));

        public ICommand AddQuestionCommand => _addQuestionCommand ??
            (_addQuestionCommand = new DelegateCommand(async () => await AddQuestion()));

        public ICommand RemoveRoundCommand => _removeRoundCommand ??
            (_removeRoundCommand = new DelegateCommand<Round>(async r => await RemoveRound(r)));

        public ICommand RemoveTopicCommand => _removeTopicCommand ??
            (_removeTopicCommand = new DelegateCommand<Topic>(async t => await RemoveTopic(t)));

        public ICommand RemoveQuestionCommand => _removeQuestionCommand ??
            (_removeQuestionCommand = new DelegateCommand<Question>(async q => await RemoveQuestion(q)));

        public ICommand OpenRoundPopupCommand => _openRoundPopupCommand ??
            (_openRoundPopupCommand = new DelegateCommand(() => RoundPopup = true));

        public ICommand OpenTopicPopupCommand => _openTopicPopupCommand ??
            (_openTopicPopupCommand = new DelegateCommand(() => TopicPopup = true));

        public ICommand OpenQuestionPopupCommand => _openQuestionPopupCommand ??
            (_openQuestionPopupCommand = new DelegateCommand(() => QuestionPopup = true));

        public ICommand CloseRoundPopupCommand => _closeRoundPopupCommand ??
        (_closeRoundPopupCommand = new DelegateCommand(() => RoundPopup = false));

        public ICommand CloseTopicPopupCommand => _closeTopicPopupCommand ??
            (_closeTopicPopupCommand = new DelegateCommand(() => TopicPopup = false));

        public ICommand CloseQuestionPopupCommand => _closeQuestionPopupCommand ??
            (_closeQuestionPopupCommand = new DelegateCommand(() => QuestionPopup = false));

        public ICommand GoBackCommand => _goBackCommand ??
            (_goBackCommand = new DelegateCommand(() => NavigationService.GoBack()));
        #endregion


        #region Override
        public override async Task OnNavigatedTo(INavigatedToEventArgs e)
        {
            var rounds = await _roundService.GetAsync();
            foreach (var round in rounds)
            {
                Rounds.Add(round);
            }

            IsPreloaderVisible = false;
        }
        #endregion


        #region Non-public Methods
        private async void UpdateTopic()
        {
            Topics.Clear();
            Questions.Clear();

            ItemTopic = null;

            if (ItemRound == null)
            {
                return;
            }

            var topics = await _topicService.GetAsync();

            foreach (var topic in topics.Where(t => t.RoundId == ItemRound.Id))
            {
                Topics.Add(topic);
            }
        }

        private async void UpdatesQuestion()
        {
            if (ItemTopic == null)
            {
                return;
            }

            Questions.Clear();
            var questions = await _questionService.GetAsync();

            foreach (var question in questions.Where(q => q.TopicId == ItemTopic.Id))
            {
                Questions.Add(question);
            }
        }

        private async Task RemoveRound(Round round)
        {
            if (round == null)
            {
                return;
            }

            var result = await _roundService.RemoveAsync(round.Id);

            if (!result)
            {
                return;
            }

            Rounds.Remove(round);
        }

        private async Task RemoveTopic(Topic topic)
        {
            if (topic == null)
            {
                return;
            }

            var result = await _topicService.RemoveAsync(topic.Id);

            if (!result)
            {
                return;
            }

            Topics.Remove(topic);
        }

        private async Task RemoveQuestion(Question question)
        {
            if (question == null)
            {
                return;
            }

            var result = await _questionService.RemoveAsync(question.Id);

            if (!result)
            {
                return;
            }


            Questions.Remove(question);
        }

        public async Task AddRound()
        {
            var round = new Round
            {
                Name = _name
            };

            var result = await _roundService.AddAsync(round);

            if (!result)
            {
                return;
            }

            Rounds.Add(round);
            RoundPopup = false;
            Name = "Название";
        }

        public async Task AddTopic()
        {
            if (ItemRound == null)
            {
                return;
            }

            var topic = new Topic
            {
                Name = _name
            };

            var result = await _topicService.AddAsync(ItemRound.Id, topic);

            if (!result)
            {
                return;
            }

            Topics.Add(topic);
            TopicPopup = false;
            Name = "Название";
        }

        public async Task AddQuestion()
        {
            if (ItemTopic == null)
            {
                return;
            }

            var question = new Question
            {
                Text = _text,
                Price = _price,
                Tag = QuestionType, 
                Answer = Answer
            };

            var result = await _questionService.AddAsync(ItemTopic.Id, question);

            if (!result)
            {
                return;
            }

            Questions.Add(question);
            QuestionPopup = false;
            Text = "Введите вопрос";
            Answer = "Введите ответ"; 
            Price = 0;
        }
        #endregion
    }
}