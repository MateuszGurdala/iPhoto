using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhoto.Commands.AccountPage
{
    public class OpenRegisterWebPageCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "http://iphotos-pap.herokuapp.com/users/register_user",
                UseShellExecute = true
            });
        }
    }
}
