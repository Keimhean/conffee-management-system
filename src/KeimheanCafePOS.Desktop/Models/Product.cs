using CommunityToolkit.Mvvm.ComponentModel;

namespace KeimheanCafePOS.Desktop.Models;

public partial class Product : ObservableObject
{
    public int Id { get; set; }

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private decimal _price;

    [ObservableProperty]
    private string _category = string.Empty;

    [ObservableProperty]
    private string _imageUrl = string.Empty;
}
