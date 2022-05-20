using iPhoto.ViewModels;
using iPhoto.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//K 13.4.22
namespace iPhoto.Commands
{
    /// <summary>
    /// Class <c>LastClickedCommand</c> determines last clicked button in side menu
    /// </summary>
    public class LastClickedCommand : CommandBase
    {
        public SideMenuButton[] ButtonsList { get; }
        public LastClickedCommand(SideMenuButton[] buttonsList)
        {
            ButtonsList = buttonsList;
        }
        public override void Execute(object parameter)
        {
            var currentButton = parameter as SideMenuButton;
            if (ButtonsList.All(i => i.LastClicked == false))
            {
                currentButton.LastClicked = true;
                currentButton.AnimateClicked();
            }
            else
            {
                foreach (SideMenuButton button in ButtonsList)
                {
                    if (button.LastClicked)
                    {
                        button.LastClicked = false;
                        button.AnimateUnclicked();
                        currentButton.LastClicked = true;
                        currentButton.AnimateClicked();
                        break;
                    }

                }

            }
        }
    }
}
