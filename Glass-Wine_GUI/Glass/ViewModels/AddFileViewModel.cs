using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
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

    private string _filePath;
    private string _fileName;
    
    public ObservableCollection<Prefix> prefixesCollection { get; } = new ObservableCollection<Prefix>();
    public ObservableCollection<WineProgram> programCollection { get; set; } = new ObservableCollection<WineProgram>();
    
    
    public AddFileViewModel()
    {
        prefixesCollection = wine.LoadPrefixes();
    }
    
    public void AssignValue(string filePath, string fileName)
    {
        _filePath = filePath;
        _fileName = fileName;
    }

    [RelayCommand]
    public void AddNewFile(Prefix prefix)
    {
        WineProgram program = new WineProgram()
        {
            name = _fileName,
            path = _filePath,
            prefix = prefix,
        };
        Console.WriteLine(FilePrefix);
        wine.StartFile(program);
        
        
        //TODO: kolekce v MainWindowViewModel
        //precist soubor
        programCollection = wine.LoadProgram();
        //zapsat 
        string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".local/share/glass");
        string programFilePath = Path.Combine(directory, "programs.json");        
        programCollection.Add(program);
        string jsonString = JsonSerializer.Serialize(programCollection);
        File.WriteAllText(programFilePath, jsonString);
    }

}
