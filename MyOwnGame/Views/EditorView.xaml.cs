using System.Windows.Controls;

namespace MyOwnGame.Views
{
    public partial class EditorView : Page
    {
        private IEditorViewModel _vm;

        public EditorView()
        {
            InitializeComponent();
        }

        public IEditorViewModel ViewModel =>
            _vm ?? (_vm = (IEditorViewModel)DataContext);

        private void DeleteRound_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel?.RemoveRoundCommand.Execute(((Button)sender).DataContext);
        }

        private void DeleteTopic_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel?.RemoveTopicCommand.Execute(((Button)sender).DataContext);
        }

        private void DeleteQuestion_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel?.RemoveQuestionCommand.Execute(((Button)sender).DataContext);
        }
    }
}