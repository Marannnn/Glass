using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Glass.Views;
using CommunityToolkit.Mvvm.Input;
using Glass.Models;
using Microsoft.VisualBasic;

namespace Glass.ViewModels;
public partial class AddFileViewModel : ViewModelBase
{
    private List<WineProgram> programList = new List<WineProgram>();
    private Wine wine = new Wine();
    private ObservableCollection<string> prefixesCollection { get; } = new ObservableCollection<string>();

    public void Test()
    {
        //TODO: Dynamically add prefixesCollection into combobox
        
        Console.WriteLine("Started :##3333");
        prefixesCollection.Add("Glass");
    }
    public void AddNewFile(string name, string path, string prefix)
    {
        Console.WriteLine($"Dostal jsem {path}");
        programList.Add(new WineProgram()
        {
            name = name,
            path = path,
            prefix = prefix
        });
        wine.StartFile(path, prefix);
    }
}

public class WineProgram
{
    public string name {get;set;}
    public string path {get;set;}
    public string prefix {get;set;}
}