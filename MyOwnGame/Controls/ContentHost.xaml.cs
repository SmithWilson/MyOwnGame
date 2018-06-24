using System.Windows;
using System.Windows.Controls;

namespace MyOwnGame.Controls
{
    public partial class ContentHost : UserControl
    {
        public ContentHost()
        {
            InitializeComponent();
        }

        public object MainContent
        {
            get => GetValue(MainContentProperty);
            set => SetValue(MainContentProperty, value);
        }

        public static readonly DependencyProperty MainContentProperty =
            DependencyProperty.Register(nameof(MainContent), typeof(object), typeof(ContentHost), new PropertyMetadata(default, (s, e) =>
            {
                var control = (ContentHost)s;
                if (!Equals(e.OldValue, e.NewValue))
                {
                    control.ContentPresenter.Content = e.NewValue;
                }
            }));

        public bool IsPreloaderVisible
        {
            get => (bool)GetValue(IsPreloaderVisibleProperty);
            set => SetValue(IsPreloaderVisibleProperty, value);
        }

        public static readonly DependencyProperty IsPreloaderVisibleProperty =
           DependencyProperty.Register(nameof(IsPreloaderVisible), typeof(bool), typeof(ContentHost), new PropertyMetadata(false, (s, e) =>
           {
               var control = (ContentHost)s;
               if (!Equals(e.OldValue, e.NewValue))
               {
                   VisualStateManager.GoToState(control, (bool)e.NewValue ? "Visible" : "Hidden", true);
               }
           }));
    }
}