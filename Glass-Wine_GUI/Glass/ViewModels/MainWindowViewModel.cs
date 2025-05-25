using System;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Glass.Views;
namespace Glass.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get;} = "Welcome to Avalonia!";
    
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
}