using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Glass.Views;
using Glass.Models;
namespace Glass.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    Wine wine = new Wine();
    Collections collections = new Collections();
    public ObservableCollection<WineProgram> programCollection { get; set; }
    public ObservableCollection<Prefix> prefixCollection { get; set; }

    public MainWindowViewModel()
    {
        string defaultDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".local/share/");
        string directory = Path.Combine(defaultDir, "glass");
        
        //creating Glass folder
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        
        string prefixFilePath = Path.Combine(directory, "prefixes.json");
		string programFilePath = Path.Combine(directory, "programs.json");
        
        
        //APPLICATIONS
		#region Creating file if doesnt exist
       	 if (!File.Exists(programFilePath)) //jestli soubor neexistuje
		{
			File.WriteAllText(programFilePath, string.Empty);
		}
		#endregion
        
        collections.LoadProgram();
        programCollection = collections.programCollection;
        		
        //PREFIXES
		#region Creating file if doesnt exist
        if (!File.Exists(prefixFilePath))  //jestli neexistuje soubor
        {
            File.WriteAllText(prefixFilePath, string.Empty);
        }
		#endregion
        collections.LoadPrefixes();      
        prefixCollection = collections.prefixCollection;
	    string userName = Environment.UserName;
        string defaultDirectory = $"/home/{userName}/.wine";
        if (Directory.Exists(defaultDirectory))		//pokud ta wine directory existuje
        {
            bool exists = prefixCollection.Any(x => x.path == defaultDirectory); // true jestli je defaultDirectory v listu
            if (!exists)	//pokud ji nemam zapsanou
            {
                Prefix prefix = new Prefix()
                {
                    path = defaultDirectory,
                };
                prefixCollection.Add(prefix);
                string jsonString = JsonSerializer.Serialize(prefixCollection);
                File.WriteAllText(prefixFilePath, jsonString);
            }
        }

    }


	//APPLICATIONS
    [RelayCommand]
    public void OpenFileWindow()
    {
        var AddFileWindow = new AddFile
        {
            DataContext = new AddFileViewModel(collections) //urci data context pro instanci okna// tady to jsem si vypujcil z app.axaml. PROC TO NEBYLO V DOKUMENTACI NEBO ASI MOZNA JO JA NEVIM
        };
        AddFileWindow.Show();
        Console.WriteLine($"New Window: {AddFileWindow}");
        
    }

    [RelayCommand]
    public void StartFile(WineProgram program)
    {
        wine.StartFile(program);
    }

    [RelayCommand]
    public void RefreshPrograms()
    {
        string defaultDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".local/share/");
        string directory = Path.Combine(defaultDir, "glass");
        string programFilePath = Path.Combine(directory, "programs.json");

        
        var toRemove = programCollection.Where(x => !Path.Exists(x.path)).ToList();

        foreach (var item in toRemove)
        {
            programCollection.Remove(item);
        }
        
        string jsonString = JsonSerializer.Serialize(programCollection);
        File.WriteAllText(programFilePath, jsonString);
    }
    
    //PREFIX
    [RelayCommand]
    public void OpenPrefixWindow()
    {
        var AddPrefixWindow = new AddPrefix
        {
            DataContext = new AddPrefixViewModel(collections)
        };
        AddPrefixWindow.Show();
        Console.WriteLine($"New Window: {AddPrefixWindow}");
    }

    [RelayCommand]
    public void RemovePrefix(Prefix prefix)
    {
        prefixCollection.Remove(prefix);
        
        //odstraneni slozky
        string folderPath = prefix.path;
        Directory.Delete(folderPath, true); //true - recursive = -r = odstrani vsechno rekuzrivne, subslozky a tak
        
        //jestli se uspesne odstranila
        if (!Directory.Exists(folderPath))
        {
            //serializace do json
            string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".local/share/glass");
            string filePath = Path.Combine(directory, "prefixes.json");

            string jsonString = JsonSerializer.Serialize(prefixCollection);
            File.WriteAllText(filePath, jsonString);
        }
    }
    [RelayCommand]
    public void RefreshPrefixes()
    {
        string defaultDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".local/share/");
        string directory = Path.Combine(defaultDir, "glass");
        string prefixesFilePath = Path.Combine(directory, "prefixes.json");

        
        var toRemove = prefixCollection.Where(x => !Path.Exists(x.path)).ToList();

        foreach (var item in toRemove)
        {
            prefixCollection.Remove(item);
        }
        
        string jsonString = JsonSerializer.Serialize(prefixCollection);
        File.WriteAllText(prefixesFilePath, jsonString);
    }
}

