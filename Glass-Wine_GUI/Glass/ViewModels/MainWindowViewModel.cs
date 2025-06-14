using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Glass.Views;
using Glass.Models;
namespace Glass.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Prefix> prefixCollection { get; } = new ObservableCollection<Prefix>();

    public MainWindowViewModel()
    {
        Wine wine = new Wine();
        string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".local/share/glass");
        string filePath = Path.Combine(directory, "prefixes.json");

        if (!File.Exists(filePath))  //jestli neexistuje soubor
        {
            File.WriteAllText(filePath, string.Empty);
        }
        ObservableCollection<Prefix> prefixes = wine.LoadPrefixes();
        
	    string userName = Environment.UserName;
        string defaultDirectory = $"/home/{userName}/.wine";
        if (Directory.Exists(defaultDirectory))		//pokud ta wine directory existuje
        {
            bool exists = prefixes.Any(x => x.path == defaultDirectory); // true jestli je defaultDirectory v listu
            if (!exists)	//pokud ji nemam zapsanou
            {
                Prefix prefix = new Prefix()
                {
                    path = defaultDirectory,
                    Architecture = AddPrefixViewModel.Architecture.win64
                };
                //TODO: SERIALIZE
                prefixes.Add(prefix);
                string jsonString = JsonSerializer.Serialize(prefixes);
                File.WriteAllText(filePath, jsonString);
            }
        }

        //TODO: path collecttion and arch
        foreach (var prefix in prefixes)
        {
            prefixCollection.Add(prefix);
        }
    }

    [RelayCommand]
    public void OpenFileWindow()
    {
        var AddFileWindow = new AddFile
        {
            DataContext = new AddFileViewModel() //urci data context pro instanci okna// tady to jsem si vypujcil z app.axaml. PROC TO NEBYLO V DOKUMENTACI NEBO ASI MOZNA JO JA NEVIM
        };
        AddFileWindow.Show();
        Console.WriteLine($"New Window: {AddFileWindow}");
        
    }

    [RelayCommand]
    public void OpenPrefixWindow()
    {
        var AddPrefixWindow = new AddPrefix
        {
            DataContext = new AddPrefixViewModel()
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
}

