using iPhoto.ViewModels;
using iPhoto.Views;
using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace iPhoto.Commands
{
    public class ExtendSideMenuCommand : CommandBase
    {
        private SideMenuView _sideMenu;
        private SideMenuViewModel _sideMenuViewModel;

        private const int _minWidth = 100;
        private const int _maxWidth = 265;
        private const int _duration = 150; //miliseconds

        public ExtendSideMenuCommand(SideMenuView sideMenu, SideMenuViewModel sideMenuViewModel)
        {
            _sideMenu = sideMenu;
            _sideMenuViewModel = sideMenuViewModel;
        }
        public override void Execute(object parameter)
        {
            if (_sideMenu.Width == _minWidth)
            {
                Extend();
            }
            else if (_sideMenu.Width == _maxWidth)
            {
                Collapse();
            }
        }
        private void Extend()
        {
            var myDoubleAnimation = new DoubleAnimation();

            myDoubleAnimation.From = _minWidth;
            myDoubleAnimation.To = _maxWidth;
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(_duration));

            var myStoryboard = new Storyboard();
            myStoryboard.Children.Add(myDoubleAnimation);
            Storyboard.SetTarget(myDoubleAnimation, _sideMenu);
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(SideMenuView.WidthProperty));

            myStoryboard.Begin();

            //_sideMenuViewModel.ExtendImage = _sideMenuViewModel.HideImageSource;
        }
        private void Collapse()
        {
            var myDoubleAnimation = new DoubleAnimation();

            myDoubleAnimation.From = _maxWidth;
            myDoubleAnimation.To = _minWidth;
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(_duration));

            var myStoryboard = new Storyboard();
            myStoryboard.Children.Add(myDoubleAnimation);
            Storyboard.SetTarget(myDoubleAnimation, _sideMenu);
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(SideMenuView.WidthProperty));

            myStoryboard.Begin();

            //_sideMenuViewModel.ExtendImage = _sideMenuViewModel.ExtendImageSource;
        }
    }
}
