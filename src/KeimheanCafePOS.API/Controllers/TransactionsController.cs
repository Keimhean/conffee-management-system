using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KeimheanCafePOS.Infrastructure.Data;
using KeimheanCafePOS.Domain.Entities;

namespace KeimheanCafePOS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<TransactionsController> _logger;

    public TransactionsController(ApplicationDbContext context, ILogger<TransactionsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
    {
        try
        {
            return await _context.Transactions
                .Include(t => t.Items)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting transactions");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Transaction>> GetTransaction(int id)
    {
        var transaction = await _context.Transactions
            .Include(t => t.Items)
            .FirstOrDefaultAsync(t => t.Id == id);
        
        if (transaction == null) return NotFound();
        return transaction;
    }

    [HttpPost]
    public async Task<ActionResult<Transaction>> CreateTransaction(Transaction transaction)
    {
        try
        {
            _logger.LogInformation("Received transaction: {@Transaction}", transaction);
            
            if (transaction.Items == null || !transaction.Items.Any())
            {
                _logger.LogWarning("Transaction has no items");
                return BadRequest("Transaction must have at least one item");
            }

            transaction.Subtotal = transaction.Items.Sum(i => i.Price * i.Quantity);
            transaction.Tax = transaction.Subtotal * 0.10m;
            transaction.Total = transaction.Subtotal + transaction.Tax;
            transaction.ChangeGiven = transaction.CashReceived - transaction.Total;
            transaction.TransactionDate = DateTime.UtcNow;

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Transaction created successfully: {Id}", transaction.Id);
            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating transaction. Details: {Message}", ex.Message);
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransaction(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null) return NotFound();
        
        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("stats")]
    public async Task<ActionResult<object>> GetStatistics()
    {
        var transactions = await _context.Transactions.Include(t => t.Items).ToListAsync();
        
        return Ok(new
        {
            totalTransactions = transactions.Count,
            totalRevenue = transactions.Sum(t => t.Total),
            averageOrder = transactions.Any() ? transactions.Average(t => t.Total) : 0,
            totalItems = transactions.Sum(t => t.Items.Sum(i => i.Quantity))
        });
    }
}
