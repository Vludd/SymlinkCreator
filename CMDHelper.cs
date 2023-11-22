using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;

namespace SymlinkCreator
{
    public static class CMDHelper
    {
        public static void CreateSymlink(string fromSource, string toSource, string arg, string linkName = "MyLink")
        {
            if (string.IsNullOrEmpty(linkName))
            {
                linkName = "MyLink";
            }

            string defaultPrompt = "/C mklink";
            string savePath = $@"{toSource}\{linkName}";
            string prompt = $"{defaultPrompt} {arg} \"{savePath}\" \"{fromSource}\"";
            
            Process cmd = new Process();

            cmd.StartInfo.FileName = "CMD.exe";
            cmd.StartInfo.Arguments = prompt;
            cmd.StartInfo.CreateNoWindow = false;
            cmd.Start();
            cmd.WaitForExit();

            MessageBox.Show($"Symlink created! Path: \"{savePath}\"");
        }
    }
}
