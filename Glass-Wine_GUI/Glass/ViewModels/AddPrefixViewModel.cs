using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text.Json;
using Avalonia.Controls.Shapes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Glass.Models;

namespace Glass.ViewModels;

public partial class AddPrefixViewModel : ViewModelBase
{
    private Wine wine = new Wine();
    private Collections collections;

    [ObservableProperty]
    private string _prefixName;
    public ObservableCollection<Prefix> prefixCollection { get; set; }

    public AddPrefixViewModel(Collections collections)
    {
        this.collections = collections;
    }
    
    [RelayCommand]
    public void NewPrefix()
    {
        if (PrefixName is not null)
        {
            wine.NewPrefix(PrefixName, collections);
        }
    }
}
