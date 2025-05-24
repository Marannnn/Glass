using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Glass.Views;

public partial class AddFile : Window
{
    public AddFile()
    {
        InitializeComponent();
    }

    private void AddButton(object? sender, RoutedEventArgs e)
    {
       
        Close(); //okno se vypne
    }

    private async void BrowseButton(object? sender, RoutedEventArgs e)
    {
        //tady to definuje novy file type protoze z nejakeho duvodu nemaji defaultne exe soubory  mozna to pozdeji presunu do jineho souboru
        var exeFiles = new FilePickerFileType(".exe")
        {
            Patterns = new []{"*.exe"},
            MimeTypes = new[] { "application/vnd.microsoft.portable-executable"}
        };
        
        var topLevel = TopLevel.GetTopLevel(this);
        //vrati list IStorageFile instancí  
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Please select an .exe file",
            AllowMultiple = false,
            FileTypeFilter = new[] {exeFiles}
        });
        // TODO: tmpFilePath = files[0].Path.ToString();
        // OTEVRIT STREAM SOUBORU, TAM POSLAT files A TEN PRECIST V AddFileViewModel
    }

}