using iPhoto.Commands.AccountPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace iPhoto.ViewModels
{
    public class AccountViewModel : ViewModelBase 
    {
        public ICommand OpenRegisterWebPageCommand { get; set; }
        public ICommand LogInCommand { get; set; }

        public AccountViewModel()
        {
            OpenRegisterWebPageCommand = new OpenRegisterWebPageCommand();
            LogInCommand = new LogInCommand();
        }
    }
}
