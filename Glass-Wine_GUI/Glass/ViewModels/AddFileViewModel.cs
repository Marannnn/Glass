using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Glass.Views;
using CommunityToolkit.Mvvm.Input;
using Glass.Models;
using Microsoft.VisualBasic;

namespace Glass.ViewModels;

public partial class AddFileViewModel : ViewModelBase
{
    private Wine wine = new Wine();
    
    [ObservableProperty]
    private string _filePrefix;
    
    public ObservableCollection<Prefix> prefixesCollection { get; } = new ObservableCollection<Prefix>();

    public AddFileViewModel()
    {
        prefixesCollection = wine.LoadPrefixes();
    }

    public void AddNewFile(string name, string path)
    {
        Console.WriteLine($"Dostal jsem {path}");
        WineProgram wineProgram = new WineProgram()
        {
            name = name,
            path = path,
            prefix = FilePrefix
        };
        wine.StartFile(wineProgram);
    }

}
