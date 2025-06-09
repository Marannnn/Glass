using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Glass.Models;

namespace Glass.ViewModels;

public partial class AddPrefixViewModel : ViewModelBase
{
    private Wine wine = new Wine();

    [ObservableProperty]
    private string _prefixName;
    [ObservableProperty]
    private Architecutre _selectedArch;
    
    public enum Architecutre
    {
            x64,
            X86
    }
    public ObservableCollection<Architecutre> architecturesCollection { get; set; } = new ObservableCollection<Architecutre>
    {
        Architecutre.X86,
        Architecutre.x64
    };
    
    [RelayCommand]
    public void NewPrefix()
    {
        if (PrefixName is not null)
        {
            wine.NewPrefix(PrefixName, SelectedArch);
        }
    }
}
