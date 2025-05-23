using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Glass.Views;

public partial class AddFile : Window
{
    public AddFile()
    {
        InitializeComponent();
    }


    private void CloseWindow(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}