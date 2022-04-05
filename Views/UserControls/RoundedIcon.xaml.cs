using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace iPhoto.Views.UserControls
{
    [ContentProperty(nameof(Children))]
    public partial class RoundedIcon : UserControl
    {
        public static readonly DependencyPropertyKey ChildrenProperty = DependencyProperty.RegisterReadOnly(
                        nameof(Children), typeof(UIElementCollection), typeof(RoundedIcon), new PropertyMetadata());
        public UIElementCollection Children
        {
            get { return (UIElementCollection)GetValue(ChildrenProperty.DependencyProperty); }
            private set { SetValue(ChildrenProperty, value); }
        }
        public RoundedIcon()
        {
            InitializeComponent();
            Children = ParentReference.Children;
        }
    }
}
