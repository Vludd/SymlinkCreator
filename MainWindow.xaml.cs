using Microsoft.Win32;
using System.Windows;

namespace SymlinkCreator
{
    public partial class MainWindow : Window
    {
        private LinkCreator linkCreator = new LinkCreator();

        private string source;
        private string savePath;
        private string linkName;

        public MainWindow()
        {
            InitializeComponent();
            sourceDisplay.Text = "File or directory not selected...";
            savePathDisplay.Text = "Directory not selected...";
        }

        private void openFromSource_Click(object sender, RoutedEventArgs e)
        {
            source = linkCreator.SelectPathToObject((bool)fromSourceOption1.IsChecked);
            sourceDisplay.Text = source;
            sourceDisplay.ToolTip = source;
        }

        private void openToSource_Click(object sender, RoutedEventArgs e)
        {
            savePath = linkCreator.SaveFrom();
            string displayText = savePath;
            displayText += string.IsNullOrEmpty(linkNameTextBox.Text) ? "" : $@"\{linkNameTextBox.Text}";
            savePathDisplay.Text = displayText;
            sourceDisplay.ToolTip = displayText;
        }

        private void createLink_Click(object sender, RoutedEventArgs e)
        {
            linkName = linkNameTextBox.Text;
            linkCreator.CreateLink(source, savePath, linkName);
        }
    }
}