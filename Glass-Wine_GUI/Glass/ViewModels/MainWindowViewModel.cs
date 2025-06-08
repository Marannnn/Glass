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

        // readas the prefix file
        List<string> prefixes = File.ReadAllLines(Path.Combine(directory, "prefixes.txt")).ToList();
        
        string defaultDirecotry = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".wine");
        if (Directory.Exists(defaultDirecotry))
        {
            if (!prefixes.Contains("~/.wine"))
            {
                //FIX: prefix doesnt append ~/.wine. so its not written into file
                prefixes.Add("~/.wine");
                foreach (string prefix in prefixes) 
                {
                    Console.WriteLine(prefix);
                }
                File.WriteAllLines(Path.Combine(directory, "prefixes.txt"), prefixes);
            }
        }

        //TODO: load all prefixes
        foreach (string prefix in prefixes)
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
}