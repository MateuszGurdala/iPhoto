using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace iPhoto.Commands
{
    public class MinimizeCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
    }
}
