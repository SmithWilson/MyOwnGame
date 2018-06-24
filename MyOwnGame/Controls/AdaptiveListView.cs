using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MyOwnGame.Controls
{
    class AdaptiveListView : ListView
    {
        public AdaptiveListView()
        {
            SizeChanged += AdaptiveListView_SizeChanged;
            Items.CurrentChanged += Items_CurrentChanged;
        }

        private double ItemWidth
        {
            get => (double)GetValue(ItemWidthProperty);
            set => SetValue(ItemWidthProperty, value);
        }

        public double ItemHeight
        {
            get => (double)GetValue(ItemHeightProperty);
            set => SetValue(ItemHeightProperty, value);
        }

        public Property Property
        {
            get => (Property)GetValue(PropertyProperty);
            set => SetValue(PropertyProperty, value);
        }

        public static readonly DependencyProperty PropertyProperty =
            DependencyProperty.Register("Property", typeof(Property), typeof(AdaptiveListView), new PropertyMetadata(default));

        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.Register("ItemHeight", typeof(double), typeof(AdaptiveListView), new PropertyMetadata(default));

        public static readonly DependencyProperty ItemWidthProperty =
            DependencyProperty.Register(nameof(ItemWidth), typeof(double), typeof(AdaptiveListView), new PropertyMetadata(default));

        private void AdaptiveListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Property == Property.Width)
            {
                RecalculateWidth(e.NewSize.Width);
            }
            else
            {
                RecalculateHeight(e.NewSize.Height);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Items_CurrentChanged(null, null); 
        }

        private void Items_CurrentChanged(object sender, System.EventArgs e)
        {
            if (Property == Property.Width)
            {
                RecalculateWidth(ActualWidth);
            }
            else
            {
                RecalculateHeight(ActualHeight);
            }
        }

        private void RecalculateWidth(double containerWidth)
        {
            containerWidth = containerWidth - Margin.Left - Margin.Right;

            if (containerWidth > 0)
            {
                ItemWidth = containerWidth / 5;
            }
        }

        private void RecalculateHeight(double containerHeight)
        {
            containerHeight = containerHeight - Margin.Top - Margin.Bottom;

            if (containerHeight > 0)
            {
                ItemHeight = containerHeight / 5;
            }
        }

        protected override void PrepareContainerForItemOverride(DependencyObject obj, object item)
        {
            base.PrepareContainerForItemOverride(obj, item);
            if (obj is FrameworkElement element)
            {
                if (Property == Property.Width)
                {
                    SetWidthBinding(element);
                }
                else
                {
                    SetHeightBinding(element);
                }
            }
        }

        private void SetWidthBinding(FrameworkElement e)
        {
            var widthBinding = new Binding()
            {
                Source = this,
                Path = new PropertyPath(nameof(ItemWidth)),
                Mode = BindingMode.TwoWay,
            };

            e.SetBinding(WidthProperty, widthBinding);
        }

        private void SetHeightBinding(FrameworkElement e)
        {
            var heightBinding = new Binding()
            {
                Source = this,
                Path = new PropertyPath(nameof(ItemHeight)),
                Mode = BindingMode.TwoWay
            };

            e.SetBinding(HeightProperty, heightBinding);
        }
    }

    public enum Property
    {
        Width,
        Height
    }
}