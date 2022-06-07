using System.Windows;
using System.Windows.Controls;

namespace wpf_utils
{
    public class LabeledControl : ContentControl
    {
        static LabeledControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LabeledControl), new FrameworkPropertyMetadata(typeof(LabeledControl)));
        }

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label",
                typeof(string),
                typeof(LabeledControl),
                new PropertyMetadata("DefaultLabel")
            );


        public bool Required
        {
            get { return (bool)GetValue(RequiredProperty); }
            set { SetValue(RequiredProperty, value); }
        }

        public static readonly DependencyProperty RequiredProperty =
            DependencyProperty.Register("Required", typeof(bool), typeof(LabeledControl), new PropertyMetadata(false));
    }
}
