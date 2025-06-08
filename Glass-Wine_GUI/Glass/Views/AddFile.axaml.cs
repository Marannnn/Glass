using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using Glass.ViewModels;

namespace Glass.Views;

public partial class AddFile : Window
{
    private string filePath;
    private string fileName;
    private string filePrefix;
    public AddFile()
    {
        InitializeComponent();    
        if (DataContext is AddFileViewModel vm)     //jestli DataContext je "AddFileViewModel", tak ulozim ten ViewModel do promene "vm"
        {
            vm.Test();
        }    
    }

    private async void BrowseButton(object? sender, RoutedEventArgs e)
    {
        //tady to definuje novy file type protoze z nejakeho duvodu nemaji defaultne exe soubory  mozna to pozdeji presunu do jineho souboru
        var exeFiles = new FilePickerFileType(".exe")
        {
            Patterns = new []{"*.exe"},
            MimeTypes = new []{"application/vnd.microsoft.portable-executable"}
        };
        
        var topLevel = TopLevel.GetTopLevel(this);
        //vrati list IStorageFile instancí  
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Please select an .exe file",
            AllowMultiple = false,
            FileTypeFilter = new[] {exeFiles}
        });
        if (files.Count == 1)   //jestli uzivatel vybral 1 soubor
        {
            fileName = files[0].Name;
            filePath = files[0].Path.AbsolutePath;
        }
    }   
    private void AddButton(object? sender, RoutedEventArgs e)
    {
        if (String.IsNullOrEmpty(filePath) == false)    //pokud neni prazdne
        {
            if (DataContext is AddFileViewModel vm)     //jestli DataContext je "AddFileViewModel", tak ulozim ten ViewModel do promene "vm"
            {
                vm.AddNewFile(fileName, filePath, filePrefix);
            }    
        }
        
        Close(); //okno se vypne
    }

}