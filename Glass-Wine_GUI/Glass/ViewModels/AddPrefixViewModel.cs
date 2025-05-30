using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Glass.ViewModels;

public partial class AddPrefixViewModel : ViewModelBase
{

    [ObservableProperty]
    private string _prefixName;
    
    private enum Architecutre
    {
            X86,
            x64
    }
    //TODO: bind enum to combobox
    private ObservableCollection<Architecutre> architecutresCollection = new ObservableCollection<Architecutre>
    {
        Architecutre.X86,
        Architecutre.x64
    };
    
    [RelayCommand]
    //TODO: creates new wine prefix with the inputed name
    public void NewPrefix()
    {
        if (PrefixName is not null)
        {
            Console.WriteLine(PrefixName);
        }
    }
}