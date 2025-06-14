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
    public void NewPrefix(string name, AddPrefixViewModel.Architecture architecture)
    {
	    string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".local/share/glass");
	    string filePath = Path.Combine(directory, "prefixes.json");
        string currentUser = Environment.UserName;  //gets the current user
        string prefixPath = $"/home/{currentUser}/.{name}";
        
        //TODO: create new prefix with /home/$USER/.{name} {architecture}
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
			 return; //metodu ukoncim, protoze uz vse udelala
		}
		Thread.Sleep(1000);
	 }
    }

    public ObservableCollection<Prefix> LoadPrefixes()
    {
	    string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".local/share/glass");
	    string filePath = Path.Combine(directory, "prefixes.json");
	    
	    //desereliazace
	    string jsonString = File.ReadAllText(filePath);
	    if (!String.IsNullOrEmpty(jsonString))
	    {
		    ObservableCollection<Prefix> prefixes = JsonSerializer.Deserialize<ObservableCollection<Prefix>>(jsonString);
		    return prefixes;
	    }
	    return new ObservableCollection<Prefix>();
    }
    
    public void StartFile(WineProgram wineProgram)
    {
	    string currentUser = Environment.UserName;  //gets the current user
	    
        //TODO: start with prefixes
        Process process = new Process()
        {
            StartInfo = new ProcessStartInfo()
            {
				FileName = "/bin/bash",
				Arguments = $"-c \"WINEPREFIX={wineProgram.prefix} wine '{wineProgram.path}' \"",
            }
        };
        Console.WriteLine($"Created new process {process.StartInfo.FileName} + {process.StartInfo.Arguments}");
        process.Start();
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
	public string prefix {get;set;}
}
