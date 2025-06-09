using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.ComponentModel;
using System.Linq;
using System.Net;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Glass.Views;
namespace Glass.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<string> prefixCollection { get; } = new ObservableCollection<string>();

    public MainWindowViewModel()
    {
        string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ".local/share/glass");

        if (!File.Exists(Path.Combine(directory, "prefixes.txt")))
        {
            File.WriteAllText(Path.Combine(directory, "prefixes.txt"), string.Empty);
        }

        // reads the prefix file
        List<string> prefixes = File.ReadAllLines(Path.Combine(directory, "prefixes.txt")).ToList();
        
	    string userName = Environment.UserName;
        string defaultDirectory = $"/home/{userName}/.wine";
        if (Directory.Exists(defaultDirectory))		//pokud ta wine directory existuje
        {
            if (!prefixes.Contains(defaultDirectory))	//pokud ji nemam zapsanou v prefixes.txt
            {
                prefixes.Add(defaultDirectory);
                File.WriteAllLines(Path.Combine(directory, "prefixes.txt"), prefixes);
            }
        }

        foreach (string prefix in prefixes)
        {
            prefixCollection.Add(prefix);
        }
	prefixes = null;   //manualne to odstranim protoze uz to nebudu pouzivat, asi zbytecne ale idk proc ne 
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
}
