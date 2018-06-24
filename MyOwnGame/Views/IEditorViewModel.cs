using System.Windows.Input;

namespace MyOwnGame.Views
{
    public interface IEditorViewModel
    {
        ICommand RemoveQuestionCommand { get; }
        ICommand RemoveRoundCommand { get; }
        ICommand RemoveTopicCommand { get; }
    }
}