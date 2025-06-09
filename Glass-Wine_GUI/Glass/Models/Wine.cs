using System;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Runtime.Intrinsics.X86;
using CommunityToolkit.Mvvm.ComponentModel;
using Glass.ViewModels;

namespace Glass.Models;

public class Wine
{       
    public void NewPrefix(string name, AddPrefixViewModel.Architecutre architecutre)
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

	 //loop - kazdou sekundu se diva jestli byla slozka vytvorena, jestli ano - zapise ji, jestli po 5 cyklich ne, nic neudela
	 for(int i = 0; i <= 5; i++)
	 {
	        if (Path.Exists(prefixPath))
		{
		    string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".local/share/glass/prefixes.txt");
		    File.AppendAllText(directory, prefixPath + Environment.NewLine);
		    return; //metodu ukoncim, protoze uz vse udelala
		}
		Thread.Sleep(1000);
	 }
	 return;
    }
    public void StartFile(string filePath, string prefixPath)
    {
        //TODO: start with prefixes
        Process process = new Process()
        {
            StartInfo = new ProcessStartInfo()
            {
				FileName = "/bin/bash",
				Arguments = $"c- wine"
            }
        };
        process.Start();
    }
}
