﻿using System;
using iPhoto.DataBase;
using iPhoto.UtilityClasses;
using Microsoft.Win32;

namespace iPhoto.Commands.SearchPage
{
    public class AddPhotoCommand : CommandBase
    {
        private readonly DatabaseHandler _databaseHandler;
        public AddPhotoCommand(DatabaseHandler databaseHandler)
        {
            _databaseHandler = databaseHandler;
        }

        public override void Execute(object parameter)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            ConfigureDialog(fileDialog);

            if (fileDialog.ShowDialog() != true)
            {
                return;
            }

            foreach (var fileName in fileDialog.FileNames)
            {
                var photoAdder = new PhotoAdder(_databaseHandler, fileName);
                photoAdder.GetPhotoData();
            }
        }
        private void ConfigureDialog(OpenFileDialog fileDialog)
        {
             fileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            fileDialog.Multiselect = true;
        }
    }
}