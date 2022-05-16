using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using iPhoto.Commands.AccountPage;

namespace iPhoto.ViewModels.AccountPage
{
    public class LogInViewModel : ViewModelBase
    {
        public ICommand OpenRegisterWebPageCommand { get; set; }
        public ICommand LogInCommand { get; set; }

        public LogInViewModel()
        {
            OpenRegisterWebPageCommand = new OpenRegisterWebPageCommand();
            LogInCommand = new LogInCommand();
        }
    }
}
