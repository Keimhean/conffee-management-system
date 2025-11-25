using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using KeimheanCafePOS.Desktop.Models;

namespace KeimheanCafePOS.Desktop.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "http://localhost:5138/api";

    public ApiService()
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };
        _httpClient = new HttpClient(handler);
    }

    // Products
    public async Task<List<Product>> GetProductsAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<Product>>($"{BaseUrl}/products") ?? new();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting products: {ex.Message}");
            return new();
        }
    }

    public async Task<Product?> GetProductAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Product>($"{BaseUrl}/products/{id}");
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(string category)
    {
        return await _httpClient.GetFromJsonAsync<List<Product>>($"{BaseUrl}/products/category/{category}") ?? new();
    }

    public async Task<List<Product>> SearchProductsAsync(string query)
    {
        return await _httpClient.GetFromJsonAsync<List<Product>>($"{BaseUrl}/products/search?query={query}") ?? new();
    }

    // Transactions
    public async Task<List<Transaction>> GetTransactionsAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<Transaction>>($"{BaseUrl}/transactions") ?? new();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting transactions: {ex.Message}");
            return new();
        }
    }

    public async Task<Transaction?> CreateTransactionAsync(Transaction transaction)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/transactions", transaction);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Transaction>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating transaction: {ex.Message}");
            return null;
        }
    }

    public async Task DeleteTransactionAsync(int id)
    {
        await _httpClient.DeleteAsync($"{BaseUrl}/transactions/{id}");
    }

    public async Task<TransactionStats?> GetStatsAsync()
    {
        return await _httpClient.GetFromJsonAsync<TransactionStats>($"{BaseUrl}/transactions/stats");
    }
}

public class TransactionStats
{
    public int TotalTransactions { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal AverageOrder { get; set; }
    public int TotalItems { get; set; }
}
