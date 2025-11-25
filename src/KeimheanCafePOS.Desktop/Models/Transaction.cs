using System;
using System.Collections.Generic;

namespace KeimheanCafePOS.Desktop.Models;

public class Transaction
{
    public int Id { get; set; }
    public DateTime TransactionDate { get; set; }
    public string CashierName { get; set; } = string.Empty;
    public decimal Subtotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
    public decimal CashReceived { get; set; }
    public decimal ChangeGiven { get; set; }
    public List<TransactionItem> Items { get; set; } = new();
}

public class TransactionItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
