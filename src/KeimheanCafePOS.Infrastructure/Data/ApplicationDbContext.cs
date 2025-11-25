using System;
using Microsoft.EntityFrameworkCore;
using KeimheanCafePOS.Domain.Entities;

namespace KeimheanCafePOS.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;
    public DbSet<TransactionItem> TransactionItems { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Category).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.HasIndex(e => e.Category);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CashierName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Subtotal).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Tax).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Total).HasColumnType("decimal(18,2)");
            entity.Property(e => e.CashReceived).HasColumnType("decimal(18,2)");
            entity.Property(e => e.ChangeGiven).HasColumnType("decimal(18,2)");

            entity.HasMany(e => e.Items)
                .WithOne(e => e.Transaction)
                .HasForeignKey(e => e.TransactionId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.TransactionDate);
        });

        modelBuilder.Entity<TransactionItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ProductName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
        });

        ConfigureUser(modelBuilder);
        SeedData(modelBuilder);
        SeedUsers(modelBuilder);
    }

    private void ConfigureUser(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.Role);
            entity.HasIndex(e => e.IsActive);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
            entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(256);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Role).IsRequired();
            entity.Property(e => e.IsActive).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
        });
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            // Coffee
            new Product { Id = 1, Name = "Espresso", Price = 2.50m, Category = "Coffee", ImageUrl = "https://images.unsplash.com/photo-1579992357154-faf4bde95b3d?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 2, Name = "Cappuccino", Price = 3.50m, Category = "Coffee", ImageUrl = "https://images.unsplash.com/photo-1534778101976-62847782c213?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 3, Name = "Latte", Price = 4.00m, Category = "Coffee", ImageUrl = "https://images.unsplash.com/photo-1561047029-3000c68339ca?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 4, Name = "Americano", Price = 2.80m, Category = "Coffee", ImageUrl = "https://images.unsplash.com/photo-1514432324607-a09d9b4aefdd?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 5, Name = "Mocha", Price = 4.50m, Category = "Coffee", ImageUrl = "https://images.unsplash.com/photo-1607260550778-aa9d29444ce1?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 6, Name = "Iced Latte", Price = 4.20m, Category = "Coffee", ImageUrl = "https://images.unsplash.com/photo-1517487881594-2787fef5ebf7?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 7, Name = "Iced Mocha", Price = 4.80m, Category = "Coffee", ImageUrl = "https://images.unsplash.com/photo-1642647391072-6a2416f048e5?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 8, Name = "Iced Americano", Price = 3.20m, Category = "Coffee", ImageUrl = "https://images.unsplash.com/photo-1662047102608-a6f2e492411f?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 9, Name = "Iced Caramel Macchiato", Price = 5.20m, Category = "Coffee", ImageUrl = "https://images.unsplash.com/photo-1599305090598-fe179d501227?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 10, Name = "Cold Brew", Price = 4.20m, Category = "Coffee", ImageUrl = "https://images.unsplash.com/photo-1517487881594-2787fef5ebf7?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            
            // Tea
            new Product { Id = 11, Name = "Green Tea", Price = 2.50m, Category = "Tea", ImageUrl = "https://images.unsplash.com/photo-1627435601361-ec25f5b1d0e5?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 12, Name = "Earl Grey", Price = 2.50m, Category = "Tea", ImageUrl = "https://images.unsplash.com/photo-1597318281699-3a108c3d1381?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 13, Name = "Chai Latte", Price = 3.80m, Category = "Tea", ImageUrl = "https://images.unsplash.com/photo-1576092768241-dec231879fc3?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 14, Name = "Matcha Latte", Price = 4.50m, Category = "Tea", ImageUrl = "https://images.unsplash.com/photo-1536013564249-9c83f0f97155?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            
            // Pastry
            new Product { Id = 15, Name = "Croissant", Price = 3.00m, Category = "Pastry", ImageUrl = "https://images.unsplash.com/photo-1555507036-ab1f4038808a?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 16, Name = "Blueberry Muffin", Price = 3.50m, Category = "Pastry", ImageUrl = "https://images.unsplash.com/photo-1607478900766-efe13248b125?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 17, Name = "Chocolate Cake", Price = 5.00m, Category = "Pastry", ImageUrl = "https://images.unsplash.com/photo-1578985545062-69928b1d9587?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 18, Name = "Cheesecake", Price = 5.50m, Category = "Pastry", ImageUrl = "https://images.unsplash.com/photo-1533134486753-c833f0ed4866?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            
            // Snacks
            new Product { Id = 19, Name = "Chocolate Chip Cookie", Price = 2.00m, Category = "Snack", ImageUrl = "https://images.unsplash.com/photo-1499636136210-6f4ee915583e?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 20, Name = "Brownie", Price = 3.50m, Category = "Snack", ImageUrl = "https://images.unsplash.com/photo-1607920591413-4ec007e70023?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 21, Name = "Macarons", Price = 4.50m, Category = "Snack", ImageUrl = "https://images.unsplash.com/photo-1569864358642-9d1684040f43?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 22, Name = "Donut", Price = 2.80m, Category = "Snack", ImageUrl = "https://images.unsplash.com/photo-1551024506-0bccd828d307?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) }
        );
    }

    private void SeedUsers(ModelBuilder modelBuilder)
    {
        const string staffPasswordHash = "10176e7b7b24d317acfcf8d2064cfd2f24e154f7b5a96603077d5ef813d6a6b6"; // staff123
        const string adminPasswordHash = "240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9"; // admin123

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "staff",
                PasswordHash = staffPasswordHash,
                FullName = "Staff Member",
                Role = UserRole.Staff,
                IsActive = true,
                Email = "staff@keimhean.cafe",
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                Id = 2,
                Username = "admin",
                PasswordHash = adminPasswordHash,
                FullName = "System Administrator",
                Role = UserRole.Admin,
                IsActive = true,
                Email = "admin@keimhean.cafe",
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
