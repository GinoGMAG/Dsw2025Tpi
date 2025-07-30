using Dsw2025Tpi.Domain.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dsw2025Tpi.Data;
public class Dsw2025TpiContext : DbContext
{
    public Dsw2025TpiContext(DbContextOptions<Dsw2025TpiContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(p =>
        {
            p.ToTable("Products")
            .Property(p => p.Sku)
            .IsRequired()
            .HasMaxLength(20)
            .IsUnicode();
            p.Property(p => p.Id)
            .IsRequired();
            p.Property(p => p.Name)
            .IsRequired();
        });

        modelBuilder.Entity<Order>(o =>
        {
            o.ToTable("Orders")
            .Property(o => o.CustomerId)
            .IsRequired();
            o.HasKey(o => o.Id);
            o.HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
            o.Property(o => o.ShippingAddress)
            .IsRequired()
            .HasMaxLength(200);
            o.Property(o => o.BillingAddress)
            .IsRequired()
            .HasMaxLength(200);
            o.Property(o => o.TotalAmount)
            .IsRequired();
        });

        modelBuilder.Entity<OrderItem>(oi =>
        {
            oi.ToTable("OrderItems")
            .Property(oi => oi.ProductID)
            .IsRequired();
            oi.HasKey(oi => oi.Id);
            oi.HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductID)
            .OnDelete(DeleteBehavior.Cascade);
            oi.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderID)
            .OnDelete(DeleteBehavior.Cascade);
            oi.Property(oi => oi.Quantity)
            .IsRequired();
            oi.Property(oi => oi.UnitPrice)
            .IsRequired();
            

        });

        modelBuilder.Entity<Customer>(c =>
        {
            c.ToTable("Customers")
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
            c.Property(c => c.Id)
            .IsRequired();
            c.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(false);
            c.Property(c => c.PhoneNumber)
            .HasMaxLength(15)
            .IsUnicode(false);
        });


    }

}