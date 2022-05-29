using System.Windows;
using System.Windows.Controls;

namespace iPhoto.Views.AccountPage
{
    /// <summary>
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (this.DataContext != null)
            {
                ((dynamic) this.DataContext).SecurePassword = passwordBox.Password;
            }
        }
    }
}
