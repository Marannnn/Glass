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

    [ObservableProperty]
    private string _prefixName;
    [ObservableProperty]
    private Architecture _selectedArch;
    
    public enum Architecture
    {
            win64,
            win32
    }
    public ObservableCollection<Architecture> architecturesCollection { get; set; } = new ObservableCollection<Architecture>
    {
        Architecture.win64,
        Architecture.win32
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
