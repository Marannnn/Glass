using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Intrinsics.X86;
using CommunityToolkit.Mvvm.ComponentModel;
using Glass.ViewModels;
using CliWrap;

namespace Glass.Models;

public class Wine
{       
    public async void NewPrefix(string name, AddPrefixViewModel.Architecutre architecutre)
    {
        string currentUser = Environment.UserName;  //gets the current user
        string prefixPath = $"/home/{currentUser}/.{name}";
        
        //TODO: create new prefix with /home/$USER/.{name} {architecture}
         Process process = new Process()
         {
             StartInfo = new ProcessStartInfo()
             {
                 FileName = "/bin/bash",
                 Arguments = $"-c \"WINEPREFIX={prefixPath} wineboot -u\"" ,
                 CreateNoWindow = true,
                 UseShellExecute = true
             }
         };
         process.Start();
         Console.WriteLine($"Created new process {process.StartInfo.FileName}");
        
        //if the folder exists(if the wine creation was succesful)
        if (Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), prefixPath)))
        {
            string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".local/share/glass");
            File.AppendAllText(Path.Combine(Path.Combine(directory, "prefixes.txt")), prefixPath + Environment.NewLine);   
        }
    }
    public void StartFile(string filePath, string prefixPath)
    {
        //TODO: start with prefixes
        Process process = new Process()
        {
            StartInfo = new ProcessStartInfo()
            {
           
            }
        };
        process.Start();
    }
}
