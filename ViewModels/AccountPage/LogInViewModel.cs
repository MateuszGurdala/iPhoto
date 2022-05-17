using System.Security;
using System.Windows.Input;
using iPhoto.Commands.AccountPage;

namespace iPhoto.ViewModels.AccountPage
{
    public class LogInViewModel : ViewModelBase
    {
        public ICommand OpenRegisterWebPageCommand { get; set; }
        public ICommand LogInCommand { get; set; }

        public string UsernameText { get; set; }
        public SecureString SecurePassword { get; set; }

        public LogInViewModel()
        {
            OpenRegisterWebPageCommand = new OpenRegisterWebPageCommand();
            LogInCommand = new LogInCommand();
        }
    }
}
