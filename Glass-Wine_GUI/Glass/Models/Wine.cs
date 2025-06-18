using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Runtime.Intrinsics.X86;
using CommunityToolkit.Mvvm.ComponentModel;
using Glass.ViewModels;

namespace Glass.Models;

public class Wine
{       
    public void NewPrefix(string name, AddPrefixViewModel.Architecture architecture, Collections collections)
    {
	    string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".local/share/glass");
	    string filePath = Path.Combine(directory, "prefixes.json");
        string currentUser = Environment.UserName;  //gets the current user
        string prefixPath = $"/home/{currentUser}/.{name}";
        
         Process process = new Process()
         {
             StartInfo = new ProcessStartInfo()
             {
                 FileName = "/bin/bash",
                 Arguments = $"-c \"WINEARCH={architecture} WINEPREFIX={prefixPath} wineboot -u\"" ,
                 CreateNoWindow = true,
             }
         };
         process.Start();
         Console.WriteLine($"Created new process {process.StartInfo.FileName}");


	 //loop - kazdou sekundu se diva jestli byla slozka vytvorena, jestli ano - zapise ji, jestli po 5 cyklich ne, nic neudela
	 for(int i = 0; i <= 5; i++)
	 {
		 if (Path.Exists(prefixPath))
		 {
			 List<Prefix> prefixes = JsonSerializer.Deserialize<List<Prefix>>(File.ReadAllText(filePath));
			 
			 Prefix prefix = new Prefix()
			 {
				 path = prefixPath,
				 Architecture = architecture
			 }; 
			 prefixes.Add(prefix);
			 string jsonString = JsonSerializer.Serialize(prefixes);
			 
			 File.WriteAllText(filePath, jsonString); 
			 collections.prefixCollection.Add(prefix);
			 return; //metodu ukoncim, protoze uz vse udelala
		}
		Thread.Sleep(1000);
	 }
    }
    
    public void StartFile(WineProgram wineProgram)
    {
	    string currentUser = Environment.UserName;  //gets the current user
	    string command = $"nohup env WINEPREFIX='{wineProgram.prefix.path}' wine \"{wineProgram.path}\" >/dev/null 2>&1";

        Process process = new Process()
        {
            StartInfo = new ProcessStartInfo()
            {
				FileName = "/bin/bash",
				Arguments = $"-c \"{command}\"",
				UseShellExecute = false,    
				RedirectStandardOutput = false,
 				RedirectStandardError = false,
            }
        };
        process.Start();
        Console.WriteLine($"Created new process {process.StartInfo.FileName} + {process.StartInfo.Arguments}");
    }



}

public class Prefix
{
	public string path { get; set; }
	public AddPrefixViewModel.Architecture Architecture {get;set;}
}

public class WineProgram
{
	public string name {get;set;}
	public string path {get;set;}
	public Prefix prefix {get;set;}
}
