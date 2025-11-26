using CommunityToolkit.Mvvm.ComponentModel;

namespace KeimheanCafePOS.Desktop.Models;

public partial class CategoryItem : ObservableObject
{
    public string Name { get; set; } = string.Empty;
    public string IconPath { get; set; } = string.Empty;

    [ObservableProperty]
    private bool _isSelected;
}
