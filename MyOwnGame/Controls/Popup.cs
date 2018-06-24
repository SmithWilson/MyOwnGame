using System.Windows;
using System.Windows.Controls;

namespace MyOwnGame.Controls
{
    public sealed class Popup : ContentControl
    {
        public Popup()
        {
            DefaultStyleKey = typeof(Popup);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Update();
        }

        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(Popup), new PropertyMetadata(false, (s, e) => ((Popup)s).Update()));

        private void Update() =>
            VisualStateManager.GoToState(this, IsOpen ? "Opened" : "Closed", true);
    }
}