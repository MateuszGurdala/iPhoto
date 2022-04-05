using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using iPhoto.ViewModels;

namespace iPhoto.Commands
{
    public class MaximizeCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            Application.Current.MainWindow.WindowState = WindowState.Maximized;

            Application.Current.MainWindow.Height = 1080;
            Application.Current.MainWindow.Width = 1920;
            Application.Current.MainWindow.Left = 0;
            Application.Current.MainWindow.Top = 0;
        }
    }
}
