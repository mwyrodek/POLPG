using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Resources;
using PolpgUI.Commands;
using PolpgUI.Configuration;
using PolpgUI.Models;

namespace PolpgUI.ViewModel
{
    public class GeneratorViewModel
    {
        public GeneratorSettingsModel GeneratorSettingsModel { get; set; }

        public Command GenerateCommand { get; set; }

        public Command CopyToClipBoardCommand { get; set; }

        private readonly PageObjectGenerator generator;

        public GeneratorViewModel()
        {
            this.generator = new PageObjectGenerator(DataManager.ReadTemplate());
            this.LoadData();
            this.GenerateCommand = new Command(this.OnGenerate, this.CanGenerate);
            GenerateCommand.RaiseCanExecuteChanged();
            this.CopyToClipBoardCommand = new Command(this.OnCopyToClipboard, this.CanCopyToClipBoard);
        }

        private void OnGenerate()
        {
            GeneratorSettingsModel.GeneratedCode = this.generator
                .SetName(this.GeneratorSettingsModel.PageName)
                .EnableInheritance(this.GeneratorSettingsModel.IsInheritance)
                .SetInheritanceValue(this.GeneratorSettingsModel.ParentName)
                .Generate();
            CopyToClipBoardCommand.RaiseCanExecuteChanged();
        }

        private void OnCopyToClipboard()
        {
            Clipboard.SetText(this.GeneratorSettingsModel.GeneratedCode);
        }

        public void LoadData()
        {
            var generatorSettingsModel = new GeneratorSettingsModel {PageName = "LoginPage"};
            this.GeneratorSettingsModel = generatorSettingsModel;
        }

        private bool CanGenerate()
        {
            return this.GeneratorSettingsModel.PageName.Length > 0;
        }

        private bool CanCopyToClipBoard()
        {
            if (this.GeneratorSettingsModel == null) return false;
            if (this.GeneratorSettingsModel.GeneratedCode == null) return false;

            return this.GeneratorSettingsModel.GeneratedCode.Length > 0;
        }
    }
}