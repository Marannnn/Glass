using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace Glass.Models;

public class Collections
{
    public ObservableCollection<WineProgram> programCollection { get; set; } = new();
    public ObservableCollection<Prefix> prefixCollection { get; set; } = new();
    
    
    //LOAD
	
    public void LoadProgram()
    {
        string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".local/share/glass");
        string filePath = Path.Combine(directory, "programs.json");
	    
        //deserializace
        string jsonString = File.ReadAllText(filePath);
        if (!String.IsNullOrEmpty(jsonString))
        {
            programCollection = JsonSerializer.Deserialize<ObservableCollection<WineProgram>>(jsonString);
        }
    }
        
    public void LoadPrefixes()
    {
        string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".local/share/glass");
        string filePath = Path.Combine(directory, "prefixes.json");
	    
        //desereliazace
        string jsonString = File.ReadAllText(filePath);
        if (!String.IsNullOrEmpty(jsonString))
        {
            prefixCollection= JsonSerializer.Deserialize<ObservableCollection<Prefix>>(jsonString);
        }
    }

    
    
}