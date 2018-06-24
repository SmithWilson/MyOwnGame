using System.Windows;

namespace MyOwnGame.Style
{
    public partial class Resources : ResourceDictionary
    {
        public Resources()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}