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
            new Product { Id = 12, Name = "Earl Grey", Price = 2.50m, Category = "Tea", ImageUrl = "https://images.unsplash.com/photo-1564890369478-c89ca6d9cde9?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 13, Name = "Chai Latte", Price = 3.80m, Category = "Tea", ImageUrl = "https://images.unsplash.com/photo-1576092768241-dec231879fc3?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 14, Name = "Matcha Latte", Price = 4.50m, Category = "Tea", ImageUrl = "https://images.unsplash.com/photo-1515823064-d6e0c04616a7?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            
            // Pastry
            new Product { Id = 15, Name = "Croissant", Price = 3.00m, Category = "Pastry", ImageUrl = "https://images.unsplash.com/photo-1555507036-ab1f4038808a?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 16, Name = "Blueberry Muffin", Price = 3.50m, Category = "Pastry", ImageUrl = "https://images.unsplash.com/photo-1607478900766-efe13248b125?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 17, Name = "Chocolate Cake", Price = 5.00m, Category = "Pastry", ImageUrl = "https://images.unsplash.com/photo-1578985545062-69928b1d9587?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 18, Name = "Cheesecake", Price = 5.50m, Category = "Pastry", ImageUrl = "https://images.unsplash.com/photo-1533134486753-c833f0ed4866?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            
            // Snacks
            new Product { Id = 19, Name = "Chocolate Chip Cookie", Price = 2.00m, Category = "Snack", ImageUrl = "https://images.unsplash.com/photo-1499636136210-6f4ee915583e?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 20, Name = "Brownie", Price = 3.50m, Category = "Snack", ImageUrl = "https://images.unsplash.com/photo-1607920591413-4ec007e70023?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 21, Name = "Macarons", Price = 4.50m, Category = "Snack", ImageUrl = "https://images.unsplash.com/photo-1569864358642-9d1684040f43?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 22, Name = "Donut", Price = 2.80m, Category = "Snack", ImageUrl = "https://images.unsplash.com/photo-1551024506-0bccd828d307?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            
            // More Coffee
            new Product { Id = 23, Name = "Flat White", Price = 3.80m, Category = "Coffee", ImageUrl = "https://images.unsplash.com/photo-1572442388796-11668a67e53d?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 24, Name = "Cortado", Price = 3.30m, Category = "Coffee", ImageUrl = "https://images.unsplash.com/photo-1511920170033-f8396924c348?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 25, Name = "Macchiato", Price = 3.00m, Category = "Coffee", ImageUrl = "https://images.unsplash.com/photo-1557006021-b85faa2bc5e2?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 26, Name = "Affogato", Price = 5.50m, Category = "Coffee", ImageUrl = "https://images.unsplash.com/photo-1578374173703-14c49d07ac86?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 27, Name = "Irish Coffee", Price = 6.50m, Category = "Coffee", ImageUrl = "https://images.unsplash.com/photo-1514066558159-fc8c737ef259?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 28, Name = "Vietnamese Coffee", Price = 4.80m, Category = "Coffee", ImageUrl = "https://images.unsplash.com/photo-1562095241-8c6714fd4178?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 29, Name = "Nitro Cold Brew", Price = 5.00m, Category = "Coffee", ImageUrl = "https://images.unsplash.com/photo-1461023058943-07fcbe16d735?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 30, Name = "Spanish Latte", Price = 4.50m, Category = "Coffee", ImageUrl = "https://images.unsplash.com/photo-1583511655857-d19b40a7a54e?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            
            // More Tea
            new Product { Id = 31, Name = "Jasmine Tea", Price = 3.00m, Category = "Tea", ImageUrl = "https://images.unsplash.com/photo-1544787219-7f47ccb76574?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 32, Name = "Oolong Tea", Price = 3.20m, Category = "Tea", ImageUrl = "https://images.unsplash.com/photo-1594631252845-29fc4cc8cde9?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 33, Name = "White Tea", Price = 3.50m, Category = "Tea", ImageUrl = "https://images.unsplash.com/photo-1571934811356-5cc061b6821f?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 34, Name = "Rooibos Tea", Price = 3.00m, Category = "Tea", ImageUrl = "https://images.unsplash.com/photo-1597481499750-3e6b22637e12?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 35, Name = "Chamomile Tea", Price = 3.00m, Category = "Tea", ImageUrl = "https://images.unsplash.com/photo-1587080266227-677cc2a4e76e?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 36, Name = "Peppermint Tea", Price = 2.80m, Category = "Tea", ImageUrl = "https://images.unsplash.com/photo-1556679343-c7306c1976bc?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 37, Name = "Khmer Iced Tea", Price = 4.00m, Category = "Tea", ImageUrl = "https://images.unsplash.com/photo-1556679343-c7306c1976bc?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 38, Name = "Bubble Tea", Price = 5.50m, Category = "Tea", ImageUrl = "https://images.unsplash.com/photo-1525385133512-2f3bdd039054?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            
            // More Pastry
            new Product { Id = 39, Name = "Pain au Chocolat", Price = 3.20m, Category = "Pastry", ImageUrl = "https://images.unsplash.com/photo-1623334044303-241021148842?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 40, Name = "Cinnamon Roll", Price = 3.80m, Category = "Pastry", ImageUrl = "https://images.unsplash.com/photo-1509440159596-0249088772ff?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 41, Name = "Danish Pastry", Price = 3.50m, Category = "Pastry", ImageUrl = "https://images.unsplash.com/photo-1586985289688-ca3cf47d3e6e?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 42, Name = "Scone", Price = 2.80m, Category = "Pastry", ImageUrl = "https://images.unsplash.com/photo-1499636136210-6f4ee915583e?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 43, Name = "Banana Bread", Price = 3.20m, Category = "Pastry", ImageUrl = "https://images.unsplash.com/photo-1593002871058-f8499c6e5d71?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 44, Name = "Carrot Cake", Price = 5.20m, Category = "Pastry", ImageUrl = "https://images.unsplash.com/photo-1621303837174-89787a7d4729?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 45, Name = "Red Velvet Cake", Price = 5.80m, Category = "Pastry", ImageUrl = "https://images.unsplash.com/photo-1586985289565-819ab7c8d03b?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 46, Name = "Tiramisu", Price = 6.00m, Category = "Pastry", ImageUrl = "https://images.unsplash.com/photo-1571877227200-a0d98ea607e9?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            
            // More Snacks
            new Product { Id = 47, Name = "Oatmeal Cookie", Price = 2.20m, Category = "Snack", ImageUrl = "https://images.unsplash.com/photo-1558961363-fa8fdf82db35?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 48, Name = "Sugar Cookie", Price = 2.00m, Category = "Snack", ImageUrl = "https://images.unsplash.com/photo-1603532648955-039310d9ed75?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 49, Name = "Peanut Butter Cookie", Price = 2.30m, Category = "Snack", ImageUrl = "https://images.unsplash.com/photo-1590080876876-6e7a1bb75d3f?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 50, Name = "Biscotti", Price = 2.50m, Category = "Snack", ImageUrl = "https://images.unsplash.com/photo-1590772134464-51bb8d4c63ad?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 51, Name = "Granola Bar", Price = 2.80m, Category = "Snack", ImageUrl = "https://images.unsplash.com/photo-1583484908420-29cd4282e6c2?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 52, Name = "Chocolate Truffle", Price = 3.80m, Category = "Snack", ImageUrl = "https://images.unsplash.com/photo-1481391243133-f96216dcb5d2?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 53, Name = "Pretzel", Price = 2.50m, Category = "Snack", ImageUrl = "https://images.unsplash.com/photo-1558636508-e0db3814bd1d?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) },
            new Product { Id = 54, Name = "Muffin Chocolate", Price = 3.50m, Category = "Snack", ImageUrl = "https://images.unsplash.com/photo-1607958996333-41aef7caefaa?w=500&q=80", CreatedAt = new DateTime(2025,11,23,9,39,43, DateTimeKind.Utc) }
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
