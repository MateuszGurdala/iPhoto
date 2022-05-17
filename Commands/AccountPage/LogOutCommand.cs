﻿using iPhoto.ViewModels.AccountPage;

namespace iPhoto.Commands.AccountPage
{
    public class LogOutCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var viewModel = parameter as LoggedInAuthViewModel;

            viewModel.AccountViewModel.CurrentViewModel = viewModel.AccountViewModel.LogInViewModel;
        }
    }
}
