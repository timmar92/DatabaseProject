using System;
using System.Collections.Generic;
using Infrastructure.Entities.ProductCatalog;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public partial class ProductDbContext : DbContext
{
    public ProductDbContext()
    {
    }

    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\WIN23\\c-sharp\\DatabaseProject\\Infrastructure\\Data\\ProductCatalogDb.mdf;Integrated Security=True;Connect Timeout=30");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2B1B99D467");

            entity.HasIndex(e => e.CategoryName, "UQ__Categori__8517B2E054DFBDCB").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(200);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B8A0A77426");

            entity.HasIndex(e => e.Email, "UQ__Customer__A9D10534728A8EF6").IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Phone)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.ManufacturerId).HasName("PK__Manufact__357E5CA1170B725E");

            entity.HasIndex(e => e.ManufacturerName, "UQ__Manufact__3B9CDE2EDEEDD34D").IsUnique();

            entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");
            entity.Property(e => e.ManufacturerName).HasMaxLength(200);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ArticleNumber).HasName("PK__Products__3C991143A7617448");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ProductName).HasMaxLength(200);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Products__Catego__41EDCAC5");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products)
                .HasForeignKey(d => d.ManufacturerId)
                .HasConstraintName("FK__Products__Manufa__42E1EEFE");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("PK__Purchase__6B0A6BDE1C4C116C");

            entity.Property(e => e.PurchaseId).HasColumnName("PurchaseID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.ProductArticleNumber).HasMaxLength(450);
            entity.Property(e => e.UnitPrice).HasColumnType("money");

            entity.HasOne(d => d.Customer).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Purchases__Custo__489AC854");

            entity.HasOne(d => d.ProductArticleNumberNavigation).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.ProductArticleNumber)
                .HasConstraintName("FK__Purchases__Produ__498EEC8D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
