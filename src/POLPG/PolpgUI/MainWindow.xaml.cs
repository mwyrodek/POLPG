﻿namespace PolpgUI
{
    using System;
    using System.Windows;
    using System.Windows.Resources;

    using PolpgUI.ViewModel;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void GeneratorViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.GeneratorViewModel generatorViewModel = new GeneratorViewModel();
            this.GeneratorViewControl.DataContext = generatorViewModel;
        }
    }
}