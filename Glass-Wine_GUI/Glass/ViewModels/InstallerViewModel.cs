using System;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.Input;
using Glass.Models;

namespace Glass.ViewModels;

public partial class InstallerViewModel
{
    Wine wine = new Wine();
    Collections collections;
    Prefix prefix;
    private string _newFileName;
    private string _newFilePath;
    public InstallerViewModel(Collections collections, Prefix prefix)
    {
        this.collections = collections;
        this.prefix = prefix;
    }

    public void AssingValue(string filePath, string fileName)
    {
        Console.WriteLine("AssingValue");
        _newFileName = fileName;
        _newFilePath = filePath;
    }

    [RelayCommand]
    public void AddNewFile()
    {
        Console.WriteLine("AddNewFile");
        WineProgram program = new WineProgram
        {
            name = _newFileName,
            path = _newFilePath,
            prefix = prefix,
        };
        Console.WriteLine(_newFilePath);
        collections.programCollection.Add(program);
    }
}

