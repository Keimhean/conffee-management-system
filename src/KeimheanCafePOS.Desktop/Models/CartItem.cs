using CommunityToolkit.Mvvm.ComponentModel;

namespace KeimheanCafePOS.Desktop.Models;

public partial class CartItem : ObservableObject
{
    public Product Product { get; set; } = null!;
    
    [ObservableProperty]
    private int _quantity;

    public decimal Subtotal => Product.Price * Quantity;

    partial void OnQuantityChanged(int value)
    {
        OnPropertyChanged(nameof(Subtotal));
    }
}
