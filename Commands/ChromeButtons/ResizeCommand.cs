using iPhoto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace iPhoto.Commands
{
    public class ResizeCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            Application.Current.MainWindow.WindowState = WindowState.Normal;
            Application.Current.MainWindow.Height = 810;
            Application.Current.MainWindow.Width = 1440;
            Application.Current.MainWindow.Left = 240;
            Application.Current.MainWindow.Top = 135;
        }
    }
}
