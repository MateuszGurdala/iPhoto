﻿using iPhoto.ViewModels;
using iPhoto.Views;

namespace iPhoto.Commands
{
    public class ClickSearchResultOptionsCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var viewModel = parameter as PhotoSearchResultViewModel;
            viewModel!.ClickSearchResultCommand.Execute(parameter);
            var popup = new PhotoSearchResultOptionsView(viewModel);
        }
    }
}
