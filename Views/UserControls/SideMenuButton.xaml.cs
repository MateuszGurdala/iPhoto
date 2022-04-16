using iPhoto.Commands;
using iPhoto.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;


namespace iPhoto.Views.UserControls
{
    public partial class SideMenuButton : UserControl
    {
        private Storyboard menuStoryboard;
        private Storyboard menuStoryboard2;
        private bool _lastClicked = false;
        private LinearGradientBrush _gradientBrush;
        public bool LastClicked
        {
            get { return _lastClicked; }
            set { _lastClicked = value; }
        }

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
            _gradientBrush = InitializeGradient();
        }


        private LinearGradientBrush InitializeGradient()
        {
            GradientStop stop1 = new GradientStop(Colors.LightGreen, 0.0);
            GradientStop stop2 = new GradientStop(Colors.Transparent, 1);
            LinearGradientBrush gradientBrush = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0.5),
                EndPoint = new Point(0, 0.5),
                GradientStops = { stop1, stop2 }
            };
            MenuButton.Background = gradientBrush;
            return gradientBrush;
        }

        // K 13.04.22 , 14.04.22
        /// <summary>
        ///  Method <m> AnimateClicked </m> enables gradient on button
        /// </summary>
        public void AnimateClicked()
        {
            GradientEnter();
            menuStoryboard.Stop();
            //TODO
            // may be useful to implement other animations in same time such as image color change
            /*            DoubleAnimation resizeButtonAnimation = new DoubleAnimation(); 
                        menuStoryboard2 = new Storyboard();
                        menuStoryboard2.Children.Add(resizeButtonAnimation);
                        resizeButtonAnimation.From = 60;
                        resizeButtonAnimation.To = 100;
                        Storyboard.SetTarget(resizeButtonAnimation, ButtonImage);
                        Storyboard.SetTargetProperty(resizeButtonAnimation, new PropertyPath(Button.HeightProperty));
                        resizeButtonAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                        menuStoryboard2.Begin();*/
        }
        /// <summary>
        ///  Method <m> AnimateUnclicked </m> disables gradient on button
        /// </summary>
        public void AnimateUnclicked()
        {
            GradientLeave();
            // may be useful to implement other animations in same time
            /*            DoubleAnimation resizeButtonAnimation = new DoubleAnimation();
                        menuStoryboard2 = new Storyboard();
                        menuStoryboard2.Children.Add(resizeButtonAnimation);
                        resizeButtonAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(100));
                        resizeButtonAnimation.From = 100;
                        resizeButtonAnimation.To = 60;
                        Storyboard.SetTarget(resizeButtonAnimation, ButtonImage);
                        Storyboard.SetTargetProperty(resizeButtonAnimation, new PropertyPath(Button.HeightProperty));
                        menuStoryboard2.Begin();*/
        }
        // K 14.04.22
        /// <summary>
        /// Method <m> GradientEnter </m> animates gradient on button
        /// </summary>
        public void GradientEnter() //MG 16.04 made public
        {
            PointAnimationUsingKeyFrames gradientAnimation = new PointAnimationUsingKeyFrames();           
            gradientAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(400));
            gradientAnimation.KeyFrames.Add(
                new LinearPointKeyFrame(
                    new Point(3, 0.5),
                    KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(400)))
                    );
            MenuButton.Background.BeginAnimation(LinearGradientBrush.EndPointProperty, gradientAnimation);
        }
        /// <summary>
        /// Method <m> GradientLeave </m> animates removing of gradient on button
        /// </summary>
        private void GradientLeave()
        {
            PointAnimationUsingKeyFrames gradientAnimation = new PointAnimationUsingKeyFrames();
            gradientAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(200));
            gradientAnimation.KeyFrames.Add(
                new LinearPointKeyFrame(
                    new Point(0, 0.5),
                    KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200)))
                    );
            MenuButton.Background.BeginAnimation(LinearGradientBrush.EndPointProperty, gradientAnimation);
        }

        public void Button_MouseEnter(object sender, MouseEventArgs e) //MG 16.04 made public
        {
            if (!LastClicked)
            {
                //GradientEnter();  MG 16.04
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
            if (!LastClicked)
            {
                GradientLeave();
            }
        }
    }
}
