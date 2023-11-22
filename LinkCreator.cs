using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace SymlinkCreator
{
    public class LinkCreator
    {
        private const string DefaultLinkName = "NewSymlink";

        private string DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private string linkName = DefaultLinkName;
        
        bool isDirectory = true;

        bool sourceSelected = false;
        bool pathSelected = false;

        public string SelectPathToObject(bool isFile)
        {
            string selectedFilePath = "File or directory not selected...";
            isDirectory = !isFile;

            try
            {
                if (isFile)
                {
                    OpenFileDialog ofd = new OpenFileDialog();

                    ofd.InitialDirectory = DefaultDirectory;
                    ofd.Filter = "All files (*.*)|*.*";
                    ofd.Title = "Select a file or a directory";

                    if (ofd.ShowDialog() == true)
                    {
                        selectedFilePath = ofd.FileName;
                        sourceSelected = true;
                    }
                }
                else
                {
                    OpenFolderDialog ofd = new OpenFolderDialog();

                    ofd.InitialDirectory = DefaultDirectory;
                    ofd.Title = "Select a directory";
                    ofd.Multiselect = false;

                    if (ofd.ShowDialog() == true)
                    {
                        selectedFilePath = ofd.FolderName;
                        sourceSelected = true;
                    }
                }

                if (string.IsNullOrEmpty(selectedFilePath))
                {
                    sourceSelected = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return selectedFilePath;
        }

        public string SaveFrom()
        {
            string selectedFilePath = "Directory not selected...";

            try
            {
                OpenFolderDialog ofd = new OpenFolderDialog();

                ofd.InitialDirectory = DefaultDirectory;
                ofd.Title = "Select a directory";
                ofd.Multiselect = false;

                if (ofd.ShowDialog() == true)
                {
                    selectedFilePath = ofd.FolderName;
                    pathSelected = true;
                }

                if (string.IsNullOrEmpty(selectedFilePath))
                {
                    pathSelected = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return selectedFilePath;
        }

        public void CreateLink(string source, string savePath, string name)
        {
            if (!sourceSelected || !pathSelected)
            {
                MessageBox.Show("File source or path to save not selected!");
                return;
            }

            string arg = "";
            linkName = name;

            if (isDirectory) { arg = "/d"; }

            CMDHelper.CreateSymlink(source, savePath, arg, linkName);
        }
    }
}
