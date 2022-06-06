using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpf_utils
{
    /// <summary>
    /// Interaction logic for LabeledControl.xaml
    /// </summary>
    public partial class LabeledControl : UserControl
    {
        public LabeledControl()
        {
            InitializeComponent();
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


        public FrameworkElement Control
        {
            get { return (FrameworkElement)GetValue(ControlProperty); }
            set { SetValue(ControlProperty, value); }
        }

        public static readonly DependencyProperty ControlProperty =
            DependencyProperty.Register("Control",
                typeof(FrameworkElement),
                typeof(LabeledControl),
                new PropertyMetadata());




        public bool Required
        {
            get { return (bool)GetValue(RequiredProperty); }
            set { SetValue(RequiredProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Required.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RequiredProperty =
            DependencyProperty.Register("Required", typeof(bool), typeof(LabeledControl), new PropertyMetadata(false, HandleRequiredPropChanged));

        private static void HandleRequiredPropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is LabeledControl ctrl && e.NewValue is bool required)
            {
                ctrl.RowLabel.FontWeight = required ? FontWeights.Bold : FontWeights.Regular;
            }
        }
    }
}
