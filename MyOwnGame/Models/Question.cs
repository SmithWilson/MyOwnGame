using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace MyOwnGame.Models
{
    /// <summary>
    /// Вопрос.
    /// </summary>
    public class Question : INotifyPropertyChanged
    {
        #region Fields
        private bool _isNotAnswered; 
        #endregion

        public Question()
        {
            IsNotAnswered = true;
        }


        #region Properties
        public int Id { get; set; }

        public string Text { get; set; }

        public string Answer { get; set; }

        public int Price { get; set; }

        public QuestionType Tag { get; set; }
        #endregion


        [NotMapped]
        public bool IsNotAnswered
        {
            get => _isNotAnswered;
            set
            {
                _isNotAnswered = value;
                OnPropertyChanged();
            }
        }


        #region Reference
        public int? TopicId { get; set; }

        public Topic Topic { get; set; }
        #endregion


        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion
    }
}