using iPhoto.Commands;
using iPhoto.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;


namespace iPhoto.Views.UserControls
{
    public partial class SideMenuButton : UserControl
    {
        private Storyboard menuStoryboard;
        private Storyboard menuStoryboard2;
        private Image lastClicked;
        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(string), typeof(SideMenuButton));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(SideMenuButton));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly DependencyProperty CommandProperty = 
            DependencyProperty.Register("Command", typeof(ICommand), typeof(SideMenuButton));

        public object Parameter
        {
            get { return (object)GetValue(ParameterProperty); }
            set { SetValue(ParameterProperty, value); }
        }
        public static readonly DependencyProperty ParameterProperty =
            DependencyProperty.Register("Parameter", typeof(object), typeof(SideMenuButton));

        public SideMenuButton()
        {
            InitializeComponent();
        }

        // K 10.04.22
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation resizeButtonAnimation = new DoubleAnimation();
            menuStoryboard2 = new Storyboard();
            menuStoryboard2.Children.Add(resizeButtonAnimation);      
            if (lastClicked == null)
            {
                lastClicked = (Image)(sender as Button).FindName("ButtonImage");
                resizeButtonAnimation.From = 60;
                resizeButtonAnimation.To = 100;
                Storyboard.SetTarget(resizeButtonAnimation, lastClicked);
                Storyboard.SetTargetProperty(resizeButtonAnimation, new PropertyPath(Button.HeightProperty));
                resizeButtonAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                menuStoryboard2.Begin();
            }
            else if (lastClicked != (Image)(sender as Button).FindName("ButtonImage"))
            {
            resizeButtonAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(100));
            resizeButtonAnimation.From = 80;
            resizeButtonAnimation.To = 60;
            Storyboard.SetTarget(resizeButtonAnimation, lastClicked);
            Storyboard.SetTargetProperty(resizeButtonAnimation, new PropertyPath(Button.HeightProperty));
            menuStoryboard2.Begin();
            lastClicked = (Image)(sender as Button).FindName("ButtonImage");
            resizeButtonAnimation.From = 60;
            resizeButtonAnimation.To = 80;
            resizeButtonAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(200));
            Storyboard.SetTarget(resizeButtonAnimation, lastClicked);
            Storyboard.SetTargetProperty(resizeButtonAnimation, new PropertyPath(Button.HeightProperty));
            menuStoryboard2.Begin();
            }
        }


        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if (lastClicked != (Image)(sender as Button).FindName("ButtonImage"))
            {
                menuStoryboard = new Storyboard
                {
                    RepeatBehavior = RepeatBehavior.Forever
                };
                ThicknessAnimation menuButtonMarginAnimation1 = new ThicknessAnimation();
                ThicknessAnimation menuButtonMarginAnimation2 = new ThicknessAnimation();
                menuButtonMarginAnimation1.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                menuButtonMarginAnimation1.From = new Thickness(0, 0, 0, 0);
                menuButtonMarginAnimation1.To = new Thickness(0, 0, 0, 15);
                menuButtonMarginAnimation2.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                menuButtonMarginAnimation2.From = new Thickness(0, 0, 0, 15);
                menuButtonMarginAnimation2.To = new Thickness(0, 0, 0, 0);
                menuButtonMarginAnimation2.BeginTime = TimeSpan.FromMilliseconds(200);
                menuStoryboard.Children.Add(menuButtonMarginAnimation1);
                menuStoryboard.Children.Add(menuButtonMarginAnimation2);
                menuStoryboard.Duration = new Duration(TimeSpan.FromMilliseconds(400));
                Storyboard.SetTarget(menuButtonMarginAnimation1, ButtonImage);
                Storyboard.SetTarget(menuButtonMarginAnimation2, ButtonImage);
                Storyboard.SetTargetProperty(menuButtonMarginAnimation1, new PropertyPath(Button.MarginProperty));
                Storyboard.SetTargetProperty(menuButtonMarginAnimation2, new PropertyPath(Button.MarginProperty));
                menuStoryboard.Begin();
            }
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            menuStoryboard.Stop();
        }
    }
}
