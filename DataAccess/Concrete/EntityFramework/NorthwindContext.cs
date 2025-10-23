using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework;

public class NorthwindContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql("Host=yourHost;Database=yourDatabase;Username=yourUserName;Password=yourPassword");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OrderDetail>(e =>
        {
            e.HasKey(od => new { od.OrderId, od.ProductId });
        });

        modelBuilder.Entity<Product>(e =>
        {
            e.ToTable("Products");
            e.Property(p => p.ProductName).HasColumnType("text");
            e.Property(p => p.UnitPrice).HasPrecision(18, 2);
        });

        modelBuilder.Entity<Category>(e =>
        {
            e.ToTable("Categories");
            e.Property(c => c.CategoryName).HasColumnType("text");
        });

        modelBuilder.Entity<Customer>(e =>
        {
            e.ToTable("Customers");
            e.Property(c => c.CompanyName).HasColumnType("text");
            e.Property(c => c.ContactName).HasColumnType("text");
            e.Property(c => c.City).HasColumnType("text");
            e.Property(c => c.CustomerId).HasColumnType("text");
        });

        modelBuilder.Entity<Order>(e =>
        {
            e.ToTable("Orders");
            e.Property(o => o.CustomerId).HasColumnType("text");
            e.Property(o => o.ShipCity).HasColumnType("text");
        });

        modelBuilder.Entity<User>(e =>
        {
            e.ToTable("Users");
            e.Property(u => u.FirstName).HasColumnType("text");
            e.Property(u => u.LastName).HasColumnType("text");
            e.Property(u => u.Email).HasColumnType("text");
        });

        modelBuilder.Entity<OperationClaim>(e =>
        {
            e.ToTable("OperationClaims");
            e.Property(o => o.Name).HasColumnType("text");
        });

        modelBuilder.Entity<UserOperationClaim>(e =>
        {
            e.ToTable("UserOperationClaims");
        });
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<User> Users { get; set; }
    
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
}
