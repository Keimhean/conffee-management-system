using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using KeimheanCafePOS.Desktop.Models;
using KeimheanCafePOS.Desktop.ViewModels;

namespace KeimheanCafePOS.Desktop.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Product_Tapped(object? sender, TappedEventArgs e)
    {
        if (sender is Border border && border.DataContext is Product product)
        {
            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.AddToCartCommand.Execute(product);
            }
        }
    }
}