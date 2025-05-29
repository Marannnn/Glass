using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Glass.ViewModels;

public partial class AddPrefixViewModel : ViewModelBase
{

    [ObservableProperty]
    private string _prefixName;
    
    //TODO: bind enum to combobox
    public enum Architecutre
    {
        X86,
        x64
    }
    
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