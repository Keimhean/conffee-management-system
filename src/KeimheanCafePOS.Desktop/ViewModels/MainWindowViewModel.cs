using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KeimheanCafePOS.Desktop.Models;
using KeimheanCafePOS.Desktop.Services;
using Avalonia;
using Avalonia.Platform;
using System.IO;
using System.Text.RegularExpressions;
using Avalonia.Threading;

namespace KeimheanCafePOS.Desktop.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly ApiService _apiService;
    private readonly ImageCacheService _imageCache = new();

    [ObservableProperty]
    private ObservableCollection<Product> _products = new();

    [ObservableProperty]
    private ObservableCollection<Product> _filteredProducts = new();

    [ObservableProperty]
    private ObservableCollection<CartItem> _cart = new();

    [ObservableProperty]
    private ObservableCollection<Transaction> _transactions = new();

    [ObservableProperty]
    private string _selectedCategory = "All";

    [ObservableProperty]
    private string _searchQuery = string.Empty;

    [ObservableProperty]
    private string _cashierName = string.Empty;

    [ObservableProperty]
    private decimal _cashAmount;

    [ObservableProperty]
    private bool _showReports;

    [ObservableProperty]
    private bool _showPayment;

    [ObservableProperty]
    private bool _orderComplete;

    [ObservableProperty]
    private TransactionStats? _stats;

    public decimal Subtotal => Cart.Sum(c => c.Subtotal);
    public decimal Tax => Subtotal * 0.10m;
    public decimal Total => Subtotal + Tax;
    public decimal Change => Math.Max(0, CashAmount - Total);
    public bool CanCompletePayment => !string.IsNullOrWhiteSpace(CashierName) && CashAmount >= Total && Cart.Count > 0;

    private const string IconBase = "avares://KeimheanCafePOS.Desktop/Assets/icons/";

    public ObservableCollection<CategoryItem> Categories { get; } = new()
    {
        new CategoryItem { Name = "All", IconPath = IconBase + "cart.svg", IsSelected = true },
        new CategoryItem { Name = "Coffee", IconPath = IconBase + "coffee-icon.svg" },
        new CategoryItem { Name = "Tea", IconPath = IconBase + "tea.svg" },
        new CategoryItem { Name = "Pastry", IconPath = IconBase + "Pastry.svg" },
        new CategoryItem { Name = "Snack", IconPath = IconBase + "Snack.svg" }
    };

    public MainWindowViewModel()
    {
        _apiService = new ApiService();
        _ = InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        // Load products first for immediate UI, then other data in parallel
        await LoadProductsAsync();
        var txTask = LoadTransactionsAsync();
        var statsTask = LoadStatsAsync();
        await Task.WhenAll(txTask, statsTask);
    }

    partial void OnSelectedCategoryChanged(string value)
    {
        FilterProducts();
    }

    partial void OnSearchQueryChanged(string value)
    {
        FilterProducts();
    }

    partial void OnCashAmountChanged(decimal value)
    {
        OnPropertyChanged(nameof(Change));
        OnPropertyChanged(nameof(CanCompletePayment));
    }

    partial void OnCashierNameChanged(string value)
    {
        OnPropertyChanged(nameof(CanCompletePayment));
    }

    private async Task LoadProductsAsync()
    {
        var products = await _apiService.GetProductsAsync();
        Products.Clear();
        foreach (var product in products)
        {
            var local = GetLocalImageUri(product.Name);
            if (!string.IsNullOrEmpty(local))
            {
                product.ImageUrl = local;
            }
            else if (!string.IsNullOrWhiteSpace(product.ImageUrl) && IsRemoteUrl(product.ImageUrl))
            {
                // Use a lighter Unsplash variant to speed up initial render
                product.ImageUrl = AdjustImageUrl(product.ImageUrl);
            }
            // Debug: log resolved image URL for troubleshooting
            try
            {
                Console.WriteLine($"[Assets] Product: {product.Name} -> ImageUrl: {product.ImageUrl}");
            }
            catch { }
            Products.Add(product);

            // In background, cache remote images and switch to local file when available
            if (!string.IsNullOrWhiteSpace(product.ImageUrl) && IsRemoteUrl(product.ImageUrl))
            {
                _ = CacheAndSwitchAsync(product);
            }
        }
        FilterProducts();
    }

    private static bool IsRemoteUrl(string url)
    {
        return url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
               url.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
    }

    private async Task CacheAndSwitchAsync(Product product)
    {
        try
        {
            var url = AdjustImageUrl(product.ImageUrl);
            var cached = await _imageCache.GetCachedPathAsync(url);
            if (cached != null)
            {
                var fileUri = new Uri(cached).AbsoluteUri;
                Dispatcher.UIThread.Post(() => { product.ImageUrl = fileUri; });
            }
        }
        catch
        {
            // ignore caching errors
        }
    }

    private string AdjustImageUrl(string url)
    {
        try
        {
            if (url.Contains("images.unsplash.com", StringComparison.OrdinalIgnoreCase))
            {
                url = ReplaceQueryParam(url, "w", "320");
                url = ReplaceQueryParam(url, "q", "60");
            }
        }
        catch { }
        return url;
    }

    private static string ReplaceQueryParam(string url, string key, string value)
    {
        try
        {
            var pattern = $"([?&]){Regex.Escape(key)}=[^&]*";
            if (Regex.IsMatch(url, pattern))
            {
                return Regex.Replace(url, pattern, $"$1{key}={value}");
            }
            return url + (url.Contains("?") ? "&" : "?") + $"{key}={value}";
        }
        catch
        {
            return url;
        }
    }

    private string? GetLocalImageUri(string productName)
    {
        if (string.IsNullOrWhiteSpace(productName)) return null;
        var sanitized = productName.Replace(" ", "");
        // Prefer raster photos (JPEG/JPG/PNG) if present, otherwise SVG placeholders
        var jpegUri = $"avares://KeimheanCafePOS.Desktop/Assets/icons/photo/{sanitized}.jpeg";
        var jpgUri = $"avares://KeimheanCafePOS.Desktop/Assets/icons/photo/{sanitized}.jpg";
        var pngUri = $"avares://KeimheanCafePOS.Desktop/Assets/icons/photo/{sanitized}.png";
        var svgUri = $"avares://KeimheanCafePOS.Desktop/Assets/icons/photo/{sanitized}.svg";
        try
        {
            if (AssetLoader.Exists(new Uri(jpegUri)))
                return jpegUri;
            if (AssetLoader.Exists(new Uri(jpgUri)))
                return jpgUri;
            if (AssetLoader.Exists(new Uri(pngUri)))
                return pngUri;
            if (AssetLoader.Exists(new Uri(svgUri)))
                return svgUri;
            // Fallback: check file on disk in project sources (helps during development)
            var repoPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", ".."));
            var svgSource = Path.Combine(repoPath, "src", "KeimheanCafePOS.Desktop", "Assets", "icons", "photo", sanitized + ".svg");
            var pngSource = Path.Combine(repoPath, "src", "KeimheanCafePOS.Desktop", "Assets", "icons", "photo", sanitized + ".png");
            var jpgSource = Path.Combine(repoPath, "src", "KeimheanCafePOS.Desktop", "Assets", "icons", "photo", sanitized + ".jpg");
            var jpegSource = Path.Combine(repoPath, "src", "KeimheanCafePOS.Desktop", "Assets", "icons", "photo", sanitized + ".jpeg");
            // Prefer returning a file:// path for raster images during development/runtime so Image can load them.
            if (File.Exists(jpegSource))
                return new Uri(jpegSource).AbsoluteUri; // file://...
            if (File.Exists(jpgSource))
                return new Uri(jpgSource).AbsoluteUri;
            if (File.Exists(pngSource))
                return new Uri(pngSource).AbsoluteUri;
            if (File.Exists(svgSource))
                return svgUri;
        }
        catch
        {
            // Ignore and fall back to remote URL.
        }
        return null;
    }

    private async Task LoadTransactionsAsync()
    {
        var transactions = await _apiService.GetTransactionsAsync();
        Transactions.Clear();
        foreach (var transaction in transactions)
        {
            Transactions.Add(transaction);
        }
    }

    private async Task LoadStatsAsync()
    {
        Stats = await _apiService.GetStatsAsync();
    }

    private void FilterProducts()
    {
        var filtered = Products.AsEnumerable();

        if (SelectedCategory != "All")
        {
            filtered = filtered.Where(p => p.Category == SelectedCategory);
        }

        if (!string.IsNullOrWhiteSpace(SearchQuery))
        {
            filtered = filtered.Where(p => 
                p.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));
        }

        FilteredProducts.Clear();
        foreach (var product in filtered)
        {
            FilteredProducts.Add(product);
        }
    }

    [RelayCommand]
    private void AddToCart(Product product)
    {
        var existingItem = Cart.FirstOrDefault(c => c.Product.Id == product.Id);
        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            Cart.Add(new CartItem { Product = product, Quantity = 1 });
        }
        UpdateTotals();
    }

    [RelayCommand]
    private void IncreaseQuantity(CartItem item)
    {
        item.Quantity++;
        UpdateTotals();
    }

    [RelayCommand]
    private void DecreaseQuantity(CartItem item)
    {
        if (item.Quantity > 1)
        {
            item.Quantity--;
        }
        else
        {
            Cart.Remove(item);
        }
        UpdateTotals();
    }

    [RelayCommand]
    private void RemoveFromCart(CartItem item)
    {
        Cart.Remove(item);
        UpdateTotals();
    }

    private void UpdateTotals()
    {
        OnPropertyChanged(nameof(Subtotal));
        OnPropertyChanged(nameof(Tax));
        OnPropertyChanged(nameof(Total));
        OnPropertyChanged(nameof(Change));
        OnPropertyChanged(nameof(CanCompletePayment));
    }

    [RelayCommand]
    private void ShowPaymentView()
    {
        if (Cart.Count == 0) return;
        ShowPayment = true;
    }

    [RelayCommand]
    private void CancelPayment()
    {
        ShowPayment = false;
        CashAmount = 0;
    }

    [RelayCommand]
    private async Task CompletePaymentAsync()
    {
        if (!CanCompletePayment) return;

        var transaction = new Transaction
        {
            CashierName = CashierName,
            CashReceived = CashAmount,
            Items = Cart.Select(c => new TransactionItem
            {
                ProductId = c.Product.Id,
                ProductName = c.Product.Name,
                Price = c.Product.Price,
                Quantity = c.Quantity
            }).ToList()
        };

        var created = await _apiService.CreateTransactionAsync(transaction);
        
        if (created != null)
        {
            OrderComplete = true;
            await Task.Delay(2000);
            
            // Reset
            Cart.Clear();
            ShowPayment = false;
            OrderComplete = false;
            CashAmount = 0;
            
            await LoadTransactionsAsync();
            await LoadStatsAsync();
        }
    }

    [RelayCommand]
    private async Task ToggleReportsAsync()
    {
        ShowReports = !ShowReports;
        if (ShowReports)
        {
            await LoadTransactionsAsync();
            await LoadStatsAsync();
        }
    }

    [RelayCommand]
    private async Task DeleteTransactionAsync(int id)
    {
        await _apiService.DeleteTransactionAsync(id);
        await LoadTransactionsAsync();
        await LoadStatsAsync();
    }

    [RelayCommand]
    private async Task RefreshDataAsync()
    {
        await LoadProductsAsync();
        await LoadTransactionsAsync();
        await LoadStatsAsync();
    }

    [RelayCommand]
    private void SelectCategory(string category)
    {
        SelectedCategory = category;
        
        // Update IsSelected for all categories
        foreach (var cat in Categories)
        {
            cat.IsSelected = cat.Name == category;
        }
    }
}


