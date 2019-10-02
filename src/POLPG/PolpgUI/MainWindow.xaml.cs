namespace PolpgUI
{
    using System;
    using System.Windows;
    using System.Windows.Resources;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PageObjectGenerator generator;

        public MainWindow()
        {
            this.InitializeComponent();
            StreamResourceInfo info = this.IntPageStreamResource();
            this.generator = new PageObjectGenerator(info);
            this.SetInitalInputValues();
        }

        private StreamResourceInfo IntPageStreamResource()
        {
            Uri uri = new Uri("Data/SimplePage.txt", UriKind.Relative);
            return Application.GetContentStream(uri);
        }

        private void SetInitalInputValues()
        {
            this.className.Text = "LoginPage";
            this.generateCodeTextBox.Text = "No Code Generated Yet";
        }

        private void GenerateCode_Click(object sender, RoutedEventArgs e)
        {
            var generatedPage = this.generator
                .SetName(this.className.Text)
                .EnableInheritance(IsInheritance.IsChecked.Value)
                .SetInheritanceValue(this.inheritance.Text)
                .Generate();
            this.generateCodeTextBox.Text = generatedPage;
        }

        private void CopyToClipBoard_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(this.generateCodeTextBox.Text);
        }

        private void IsInheritance_Checked(object sender, RoutedEventArgs e)
        {
            inheritance.IsEnabled = true;
        }
    }
}