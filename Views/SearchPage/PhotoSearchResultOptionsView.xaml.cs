﻿using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

namespace iPhoto.Views
{
    public partial class PhotoSearchResultOptionsView : Popup
    {
        private const double _maxHeight = 240;
        private const double _minHeight = 25;
        private const double _maxWidth = 160;
        private const int _duration = 150;

        public PhotoSearchResultOptionsView()
        {
            InitializeComponent();
        }
        private void AnimateEntrance(object sender, EventArgs e)
        {

            var myDoubleAnimation = new DoubleAnimation()
            {
                From = 0,
                To = _maxWidth,
                Duration = new Duration(TimeSpan.FromMilliseconds(_duration))
            };

            var myStoryboard = new Storyboard();
            myStoryboard.Children.Add(myDoubleAnimation);
            Storyboard.SetTarget(myDoubleAnimation, this);
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(WidthProperty));
            myStoryboard.Completed += AnimateHeight;

            myStoryboard.Begin();
        }
        private void AnimateHeight(object sender, EventArgs e)
        {
            var myDoubleAnimation = new DoubleAnimation()
            {
                From = _minHeight,
                To = _maxHeight,
                Duration = new Duration(TimeSpan.FromMilliseconds(_duration))
            };

            var myStoryboard = new Storyboard();
            myStoryboard.Children.Add(myDoubleAnimation);
            Storyboard.SetTarget(myDoubleAnimation, this);
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(HeightProperty));

            myStoryboard.Begin();
        }
        private void AnimateLeave(object sender, EventArgs e)
        {
            var myDoubleAnimation = new DoubleAnimation()
            {
                From = _maxHeight,
                To = _minHeight,
                Duration = new Duration(TimeSpan.FromMilliseconds(0.75 * _duration))
            };

            var myStoryboard = new Storyboard();
            myStoryboard.Children.Add(myDoubleAnimation);
            Storyboard.SetTarget(myDoubleAnimation, this);
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(SideMenuView.HeightProperty));
            myStoryboard.Completed += AnimateWidth;


            myStoryboard.Begin();
        }
        private void AnimateWidth(object sender, EventArgs e)
        {
            var myDoubleAnimation = new DoubleAnimation()
            {
                From = _maxWidth,
                To = 0,
                Duration = new Duration(TimeSpan.FromMilliseconds(0.75 * _duration))
            };

            var myStoryboard = new Storyboard();
            myStoryboard.Children.Add(myDoubleAnimation);
            Storyboard.SetTarget(myDoubleAnimation, this);
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(SideMenuView.WidthProperty));
            myStoryboard.Completed += RemovePopup;

            myStoryboard.Begin();
        }

        private void RemovePopup(object sender, EventArgs e)
        {
            IsOpen = false;
        }
    }
}