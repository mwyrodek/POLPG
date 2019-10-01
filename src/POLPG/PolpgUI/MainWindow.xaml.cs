using CodeGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PolpgUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            IntCodeWindow();
        }

        private void IntCodeWindow()
        {
            generateCodeTextBox.Text = string.Empty;
            generateCodeTextBox.AppendText("This is Mock for \n");
            generateCodeTextBox.AppendText("Future Code  \n");
            generateCodeTextBox.AppendText("That Will be generated from teplates \n");
            generateCodeTextBox.AppendText("We will see how it works");
            generateCodeTextBox.AppendText("Or if i can copy it");
        }

        private void GenerateCode_Click(object sender, RoutedEventArgs e)
        {
            generateCodeTextBox.Text = Generator.ReturnStubCode();
        }

        private void CopyToClipBoard_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(generateCodeTextBox.Text);
        }
    }
}
