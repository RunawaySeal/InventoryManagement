using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data.Entities;
using InventoryManagement.Data.Enums;

namespace InventoryManagement.Data.Context
{
  public class InventoryManagementDbContext : DbContext
  {
    public InventoryManagementDbContext(DbContextOptions<InventoryManagementDbContext> options)
        : base(options)
    {
    }

    // DbSets
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Invoice> Invoices { get; set; } = null!;
    public DbSet<InvoiceItem> InvoiceItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Configure User entity
      ConfigureUser(modelBuilder);

      // Configure Product entity
      ConfigureProduct(modelBuilder);

      // Configure Invoice entity
      ConfigureInvoice(modelBuilder);

      // Configure InvoiceItem entity
      ConfigureInvoiceItem(modelBuilder);
    }

    private void ConfigureUser(ModelBuilder modelBuilder)
    {
      var userEntity = modelBuilder.Entity<User>();

      // Primary key
      userEntity.HasKey(u => u.Id);

      // Properties
      userEntity.Property(u => u.Username)
          .IsRequired()
          .HasMaxLength(100);

      userEntity.Property(u => u.Email)
          .IsRequired()
          .HasMaxLength(255);

      userEntity.Property(u => u.PasswordHash)
          .IsRequired()
          .HasMaxLength(255);

      userEntity.Property(u => u.FirstName)
          .HasMaxLength(100);

      userEntity.Property(u => u.LastName)
          .HasMaxLength(100);

      userEntity.Property(u => u.Role)
          .IsRequired()
          .HasConversion<int>();

      userEntity.Property(u => u.IsActive)
          .IsRequired()
          .HasDefaultValue(true);

      userEntity.Property(u => u.CreatedAt)
          .IsRequired()
          .HasDefaultValueSql("datetime('now')");

      userEntity.Property(u => u.UpdatedAt);

      // Indexes
      userEntity.HasIndex(u => u.Username)
          .IsUnique()
          .HasDatabaseName("IX_Users_Username");

      userEntity.HasIndex(u => u.Email)
          .IsUnique()
          .HasDatabaseName("IX_Users_Email");

      userEntity.HasIndex(u => u.IsActive)
          .HasDatabaseName("IX_Users_IsActive");

      // Relationships
      userEntity.HasMany(u => u.Invoices)
          .WithOne(i => i.CreatedByUser)
          .HasForeignKey(i => i.CreatedByUserId)
          .OnDelete(DeleteBehavior.Restrict);
    }

    private void ConfigureProduct(ModelBuilder modelBuilder)
    {
      var productEntity = modelBuilder.Entity<Product>();

      // Primary key
      productEntity.HasKey(p => p.Id);

      // Properties
      productEntity.Property(p => p.Name)
          .IsRequired()
          .HasMaxLength(100);

      productEntity.Property(p => p.Description)
          .HasMaxLength(500);

      productEntity.Property(p => p.SKU)
          .IsRequired()
          .HasMaxLength(50);

      productEntity.Property(p => p.Price)
          .IsRequired()
          .HasColumnType("decimal(18,2)");

      productEntity.Property(p => p.Cost)
          .IsRequired()
          .HasColumnType("decimal(18,2)");

      productEntity.Property(p => p.StockQuantity)
          .IsRequired();

      productEntity.Property(p => p.MinimumStockLevel)
          .HasDefaultValue(0);

      productEntity.Property(p => p.Category)
          .HasMaxLength(100);

      productEntity.Property(p => p.Brand)
          .HasMaxLength(100);

      productEntity.Property(p => p.Unit)
          .HasMaxLength(50)
          .HasDefaultValue(ProductUnit.Piece);

      productEntity.Property(p => p.IsActive)
          .IsRequired()
          .HasDefaultValue(true);

      productEntity.Property(p => p.CreatedAt)
          .IsRequired()
          .HasDefaultValueSql("datetime('now')");

      productEntity.Property(p => p.UpdatedAt);

      // Indexes
      productEntity.HasIndex(p => p.SKU)
          .IsUnique()
          .HasDatabaseName("IX_Products_SKU");

      productEntity.HasIndex(p => p.Name)
          .HasDatabaseName("IX_Products_Name");

      productEntity.HasIndex(p => p.Category)
          .HasDatabaseName("IX_Products_Category");

      productEntity.HasIndex(p => p.IsActive)
          .HasDatabaseName("IX_Products_IsActive");

      productEntity.HasIndex(p => p.StockQuantity)
          .HasDatabaseName("IX_Products_StockQuantity");

      // Relationships
      productEntity.HasMany(p => p.InvoiceItems)
          .WithOne(ii => ii.Product)
          .HasForeignKey(ii => ii.ProductId)
          .OnDelete(DeleteBehavior.Restrict);
    }

    private void ConfigureInvoice(ModelBuilder modelBuilder)
    {
      var invoiceEntity = modelBuilder.Entity<Invoice>();

      // Primary key
      invoiceEntity.HasKey(i => i.Id);

      // Properties
      invoiceEntity.Property(i => i.InvoiceNumber)
          .IsRequired()
          .HasMaxLength(50);

      invoiceEntity.Property(i => i.InvoiceDate)
          .IsRequired();

      invoiceEntity.Property(i => i.DueDate);

      invoiceEntity.Property(i => i.CustomerName)
          .IsRequired()
          .HasMaxLength(200);

      invoiceEntity.Property(i => i.CustomerEmail)
          .HasMaxLength(255);

      invoiceEntity.Property(i => i.CustomerPhone)
          .HasMaxLength(20);

      invoiceEntity.Property(i => i.CustomerAddress)
          .HasMaxLength(500);

      invoiceEntity.Property(i => i.Status)
          .IsRequired()
          .HasConversion<int>()
          .HasDefaultValue(InvoiceStatus.Draft);

      invoiceEntity.Property(i => i.SubTotal)
          .HasColumnType("decimal(18,2)")
          .HasDefaultValue(0);

      invoiceEntity.Property(i => i.TaxAmount)
          .HasColumnType("decimal(18,2)")
          .HasDefaultValue(0);

      invoiceEntity.Property(i => i.DiscountAmount)
          .HasColumnType("decimal(18,2)")
          .HasDefaultValue(0);

      invoiceEntity.Property(i => i.TotalAmount)
          .HasColumnType("decimal(18,2)")
          .HasDefaultValue(0);

      invoiceEntity.Property(i => i.Notes)
          .HasMaxLength(1000);

      invoiceEntity.Property(i => i.CreatedAt)
          .IsRequired()
          .HasDefaultValueSql("datetime('now')");

      invoiceEntity.Property(i => i.UpdatedAt);

      invoiceEntity.Property(i => i.CreatedByUserId)
          .IsRequired();

      // Indexes
      invoiceEntity.HasIndex(i => i.InvoiceNumber)
          .IsUnique()
          .HasDatabaseName("IX_Invoices_InvoiceNumber");

      invoiceEntity.HasIndex(i => i.Status)
          .HasDatabaseName("IX_Invoices_Status");

      invoiceEntity.HasIndex(i => i.InvoiceDate)
          .HasDatabaseName("IX_Invoices_InvoiceDate");

      invoiceEntity.HasIndex(i => i.DueDate)
          .HasDatabaseName("IX_Invoices_DueDate");

      invoiceEntity.HasIndex(i => i.CreatedByUserId)
          .HasDatabaseName("IX_Invoices_CreatedByUserId");

      // Relationships
      invoiceEntity.HasOne(i => i.CreatedByUser)
          .WithMany(u => u.Invoices)
          .HasForeignKey(i => i.CreatedByUserId)
          .OnDelete(DeleteBehavior.Restrict);

      invoiceEntity.HasMany(i => i.InvoiceItems)
          .WithOne(ii => ii.Invoice)
          .HasForeignKey(ii => ii.InvoiceId)
          .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureInvoiceItem(ModelBuilder modelBuilder)
    {
      var invoiceItemEntity = modelBuilder.Entity<InvoiceItem>();

      // Primary key
      invoiceItemEntity.HasKey(ii => ii.Id);

      // Properties
      invoiceItemEntity.Property(ii => ii.Quantity)
          .IsRequired();

      invoiceItemEntity.Property(ii => ii.UnitPrice)
          .IsRequired()
          .HasColumnType("decimal(18,2)");

      invoiceItemEntity.Property(ii => ii.DiscountAmount)
          .HasColumnType("decimal(18,2)")
          .HasDefaultValue(0);

      invoiceItemEntity.Property(ii => ii.Description)
          .HasMaxLength(500);

      invoiceItemEntity.Property(ii => ii.CreatedAt)
          .IsRequired()
          .HasDefaultValueSql("datetime('now')");

      invoiceItemEntity.Property(ii => ii.InvoiceId)
          .IsRequired();

      invoiceItemEntity.Property(ii => ii.ProductId)
          .IsRequired();

      // Indexes
      invoiceItemEntity.HasIndex(ii => ii.InvoiceId)
          .HasDatabaseName("IX_InvoiceItems_InvoiceId");

      invoiceItemEntity.HasIndex(ii => ii.ProductId)
          .HasDatabaseName("IX_InvoiceItems_ProductId");

      // Relationships
      invoiceItemEntity.HasOne(ii => ii.Invoice)
          .WithMany(i => i.InvoiceItems)
          .HasForeignKey(ii => ii.InvoiceId)
          .OnDelete(DeleteBehavior.Cascade);

      invoiceItemEntity.HasOne(ii => ii.Product)
          .WithMany(p => p.InvoiceItems)
          .HasForeignKey(ii => ii.ProductId)
          .OnDelete(DeleteBehavior.Restrict);
    }

    public override int SaveChanges()
    {
      UpdateTimestamps();
      return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      UpdateTimestamps();
      return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
      var entries = ChangeTracker.Entries()
          .Where(e => e.State == EntityState.Modified);

      foreach (var entry in entries)
      {
        if (entry.Entity is User user)
        {
          user.UpdatedAt = DateTime.UtcNow;
        }
        else if (entry.Entity is Product product)
        {
          product.UpdatedAt = DateTime.UtcNow;
        }
        else if (entry.Entity is Invoice invoice)
        {
          invoice.UpdatedAt = DateTime.UtcNow;
        }
      }
    }
  }
}