using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data.Context;
using InventoryManagement.Data.Entities;
using InventoryManagement.Data.Enums;
using System.Security.Cryptography;
using System.Text;

namespace InventoryManagement.Data.Seeders
{
  public static class DbSeeder
  {
    public static async Task SeedAsync(InventoryManagementDbContext context)
    {
      // Ensure database is created
      await context.Database.EnsureCreatedAsync();

      // Seed Users
      await SeedUsersAsync(context);

      // Seed Products
      await SeedProductsAsync(context);

      // Seed Invoices
      await SeedInvoicesAsync(context);

      // Save all changes
      await context.SaveChangesAsync();
    }

    private static async Task SeedUsersAsync(InventoryManagementDbContext context)
    {
      if (await context.Users.AnyAsync())
        return; // Users already seeded

      var users = new List<User>
      {
        new User
        {
          Username = "admin",
          Email = "admin@inventorymanagement.com",
          PasswordHash = HashPassword("Admin123!"),
          FirstName = "System",
          LastName = "Administrator",
          Role = UserRole.Admin,
          IsActive = true,
          CreatedAt = DateTime.UtcNow
        },
        new User
        {
          Username = "manager",
          Email = "manager@inventorymanagement.com",
          PasswordHash = HashPassword("Manager123!"),
          FirstName = "John",
          LastName = "Manager",
          Role = UserRole.Manager,
          IsActive = true,
          CreatedAt = DateTime.UtcNow
        },
        new User
        {
          Username = "user1",
          Email = "user1@inventorymanagement.com",
          PasswordHash = HashPassword("User123!"),
          FirstName = "Jane",
          LastName = "Smith",
          Role = UserRole.User,
          IsActive = true,
          CreatedAt = DateTime.UtcNow
        },
        new User
        {
          Username = "viewer",
          Email = "viewer@inventorymanagement.com",
          PasswordHash = HashPassword("Viewer123!"),
          FirstName = "Bob",
          LastName = "Viewer",
          Role = UserRole.Viewer,
          IsActive = true,
          CreatedAt = DateTime.UtcNow
        }
      };

      await context.Users.AddRangeAsync(users);
      await context.SaveChangesAsync();
    }

    private static async Task SeedProductsAsync(InventoryManagementDbContext context)
    {
      if (await context.Products.AnyAsync())
        return; // Products already seeded

      var products = new List<Product>
      {
        // Electronics
        new Product
        {
          Name = "Laptop Dell XPS 13",
          Description = "High-performance ultrabook with Intel Core i7",
          SKU = "DELL-XPS13-001",
          Price = 1299.99m,
          Cost = 899.99m,
          StockQuantity = 25,
          MinimumStockLevel = 5,
          Category = ProductCategory.Electronics,
          Brand = "Dell",
          Unit = ProductUnit.Piece,
          IsActive = true,
          CreatedAt = DateTime.UtcNow
        },
        new Product
        {
          Name = "iPhone 15 Pro",
          Description = "Latest iPhone with advanced camera system",
          SKU = "APPLE-IP15P-001",
          Price = 999.99m,
          Cost = 699.99m,
          StockQuantity = 50,
          MinimumStockLevel = 10,
          Category = ProductCategory.Electronics,
          Brand = "Apple",
          Unit = ProductUnit.Piece,
          IsActive = true,
          CreatedAt = DateTime.UtcNow
        },
        new Product
        {
          Name = "Samsung 4K Monitor",
          Description = "32-inch 4K UHD monitor with HDR support",
          SKU = "SAMSUNG-MON32-001",
          Price = 349.99m,
          Cost = 249.99m,
          StockQuantity = 15,
          MinimumStockLevel = 3,
          Category = ProductCategory.Electronics,
          Brand = "Samsung",
          Unit = ProductUnit.Piece,
          IsActive = true,
          CreatedAt = DateTime.UtcNow
        },
        
        // Clothing
        new Product
        {
          Name = "Men's Cotton T-Shirt",
          Description = "Comfortable 100% cotton t-shirt in various colors",
          SKU = "CLOTH-TSHIRT-001",
          Price = 19.99m,
          Cost = 8.99m,
          StockQuantity = 100,
          MinimumStockLevel = 20,
          Category = ProductCategory.Clothing,
          Brand = "BasicWear",
          Unit = ProductUnit.Piece,
          IsActive = true,
          CreatedAt = DateTime.UtcNow
        },
        new Product
        {
          Name = "Women's Jeans",
          Description = "Slim-fit denim jeans with stretch fabric",
          SKU = "CLOTH-JEANS-001",
          Price = 79.99m,
          Cost = 39.99m,
          StockQuantity = 75,
          MinimumStockLevel = 15,
          Category = ProductCategory.Clothing,
          Brand = "DenimCo",
          Unit = ProductUnit.Piece,
          IsActive = true,
          CreatedAt = DateTime.UtcNow
        },

        // Books
        new Product
        {
          Name = "C# Programming Guide",
          Description = "Comprehensive guide to C# programming language",
          SKU = "BOOK-CSHARP-001",
          Price = 49.99m,
          Cost = 24.99m,
          StockQuantity = 30,
          MinimumStockLevel = 5,
          Category = ProductCategory.Books,
          Brand = "TechBooks",
          Unit = ProductUnit.Piece,
          IsActive = true,
          CreatedAt = DateTime.UtcNow
        },

        // Home & Garden
        new Product
        {
          Name = "Coffee Maker",
          Description = "Programmable coffee maker with thermal carafe",
          SKU = "HOME-COFFEE-001",
          Price = 89.99m,
          Cost = 54.99m,
          StockQuantity = 20,
          MinimumStockLevel = 5,
          Category = ProductCategory.HomeAndGarden,
          Brand = "BrewMaster",
          Unit = ProductUnit.Piece,
          IsActive = true,
          CreatedAt = DateTime.UtcNow
        },
        new Product
        {
          Name = "Garden Hose 50ft",
          Description = "Heavy-duty garden hose with spray nozzle",
          SKU = "GARDEN-HOSE-001",
          Price = 39.99m,
          Cost = 19.99m,
          StockQuantity = 40,
          MinimumStockLevel = 8,
          Category = ProductCategory.HomeAndGarden,
          Brand = "GardenPro",
          Unit = ProductUnit.Piece,
          IsActive = true,
          CreatedAt = DateTime.UtcNow
        },

        // Sports & Outdoors
        new Product
        {
          Name = "Yoga Mat",
          Description = "Non-slip exercise yoga mat with carrying strap",
          SKU = "SPORT-YOGA-001",
          Price = 29.99m,
          Cost = 14.99m,
          StockQuantity = 60,
          MinimumStockLevel = 12,
          Category = ProductCategory.SportsAndOutdoors,
          Brand = "FitLife",
          Unit = ProductUnit.Piece,
          IsActive = true,
          CreatedAt = DateTime.UtcNow
        },

        // Health & Beauty
        new Product
        {
          Name = "Face Moisturizer",
          Description = "Daily hydrating face moisturizer with SPF 30",
          SKU = "BEAUTY-MOIST-001",
          Price = 24.99m,
          Cost = 12.99m,
          StockQuantity = 80,
          MinimumStockLevel = 15,
          Category = ProductCategory.HealthAndBeauty,
          Brand = "SkinCare Pro",
          Unit = ProductUnit.Piece,
          IsActive = true,
          CreatedAt = DateTime.UtcNow
        },

        // Low stock item for testing
        new Product
        {
          Name = "Wireless Mouse",
          Description = "Ergonomic wireless mouse with long battery life",
          SKU = "TECH-MOUSE-001",
          Price = 25.99m,
          Cost = 15.99m,
          StockQuantity = 2, // Low stock
          MinimumStockLevel = 5,
          Category = ProductCategory.Electronics,
          Brand = "TechGear",
          Unit = ProductUnit.Piece,
          IsActive = true,
          CreatedAt = DateTime.UtcNow
        }
      };

      await context.Products.AddRangeAsync(products);
      await context.SaveChangesAsync();
    }

    private static async Task SeedInvoicesAsync(InventoryManagementDbContext context)
    {
      if (await context.Invoices.AnyAsync())
        return; // Invoices already seeded

      var users = await context.Users.ToListAsync();
      var products = await context.Products.ToListAsync();

      if (!users.Any() || !products.Any())
        return;

      var adminUser = users.First(u => u.Role == UserRole.Admin);
      var managerUser = users.First(u => u.Role == UserRole.Manager);

      var invoices = new List<Invoice>
      {
        // Paid Invoice
        new Invoice
        {
          InvoiceNumber = "INV-2025-001",
          InvoiceDate = DateTime.UtcNow.AddDays(-30),
          DueDate = DateTime.UtcNow.AddDays(-15),
          CustomerName = "ABC Corporation",
          CustomerEmail = "orders@abccorp.com",
          CustomerPhone = "+1-555-0123",
          CustomerAddress = "123 Business Ave, City, State 12345",
          Status = InvoiceStatus.Paid,
          SubTotal = 1649.98m,
          TaxAmount = 164.98m,
          DiscountAmount = 50.00m,
          TotalAmount = 1764.96m,
          Notes = "Bulk order for office equipment",
          CreatedAt = DateTime.UtcNow.AddDays(-30),
          CreatedByUserId = adminUser.Id
        },
        
        // Pending Invoice
        new Invoice
        {
          InvoiceNumber = "INV-2025-002",
          InvoiceDate = DateTime.UtcNow.AddDays(-15),
          DueDate = DateTime.UtcNow.AddDays(15),
          CustomerName = "XYZ Retail Store",
          CustomerEmail = "purchasing@xyzstore.com",
          CustomerPhone = "+1-555-0456",
          CustomerAddress = "456 Retail Blvd, City, State 67890",
          Status = InvoiceStatus.Sent,
          SubTotal = 399.96m,
          TaxAmount = 39.99m,
          DiscountAmount = 0m,
          TotalAmount = 439.95m,
          Notes = "Monthly inventory restocking",
          CreatedAt = DateTime.UtcNow.AddDays(-15),
          CreatedByUserId = managerUser.Id
        },

        // Overdue Invoice
        new Invoice
        {
          InvoiceNumber = "INV-2025-003",
          InvoiceDate = DateTime.UtcNow.AddDays(-45),
          DueDate = DateTime.UtcNow.AddDays(-10),
          CustomerName = "Small Business LLC",
          CustomerEmail = "finance@smallbiz.com",
          CustomerPhone = "+1-555-0789",
          CustomerAddress = "789 Commerce St, City, State 13579",
          Status = InvoiceStatus.Overdue,
          SubTotal = 159.98m,
          TaxAmount = 15.99m,
          DiscountAmount = 0m,
          TotalAmount = 175.97m,
          Notes = "Follow up required for payment",
          CreatedAt = DateTime.UtcNow.AddDays(-45),
          CreatedByUserId = adminUser.Id
        },

        // Draft Invoice
        new Invoice
        {
          InvoiceNumber = "INV-2025-004",
          InvoiceDate = DateTime.UtcNow,
          DueDate = DateTime.UtcNow.AddDays(30),
          CustomerName = "Tech Startup Inc",
          CustomerEmail = "orders@techstartup.com",
          CustomerPhone = "+1-555-0321",
          CustomerAddress = "321 Innovation Dr, City, State 24680",
          Status = InvoiceStatus.Draft,
          SubTotal = 0m,
          TaxAmount = 0m,
          DiscountAmount = 0m,
          TotalAmount = 0m,
          Notes = "Draft - pending item selection",
          CreatedAt = DateTime.UtcNow,
          CreatedByUserId = managerUser.Id
        }
      };

      await context.Invoices.AddRangeAsync(invoices);
      await context.SaveChangesAsync();

      // Add Invoice Items for completed invoices
      var paidInvoice = invoices.First(i => i.InvoiceNumber == "INV-2025-001");
      var pendingInvoice = invoices.First(i => i.InvoiceNumber == "INV-2025-002");
      var overdueInvoice = invoices.First(i => i.InvoiceNumber == "INV-2025-003");

      var invoiceItems = new List<InvoiceItem>
      {
        // Items for paid invoice
        new InvoiceItem
        {
          InvoiceId = paidInvoice.Id,
          ProductId = products.First(p => p.SKU == "DELL-XPS13-001").Id,
          Quantity = 1,
          UnitPrice = 1299.99m,
          DiscountAmount = 50.00m,
          Description = "Laptop for office use",
          CreatedAt = DateTime.UtcNow.AddDays(-30)
        },
        new InvoiceItem
        {
          InvoiceId = paidInvoice.Id,
          ProductId = products.First(p => p.SKU == "SAMSUNG-MON32-001").Id,
          Quantity = 1,
          UnitPrice = 349.99m,
          DiscountAmount = 0m,
          Description = "Monitor for workstation",
          CreatedAt = DateTime.UtcNow.AddDays(-30)
        },

        // Items for pending invoice
        new InvoiceItem
        {
          InvoiceId = pendingInvoice.Id,
          ProductId = products.First(p => p.SKU == "CLOTH-TSHIRT-001").Id,
          Quantity = 10,
          UnitPrice = 19.99m,
          DiscountAmount = 0m,
          Description = "Staff uniforms",
          CreatedAt = DateTime.UtcNow.AddDays(-15)
        },
        new InvoiceItem
        {
          InvoiceId = pendingInvoice.Id,
          ProductId = products.First(p => p.SKU == "CLOTH-JEANS-001").Id,
          Quantity = 5,
          UnitPrice = 79.99m,
          DiscountAmount = 0m,
          Description = "Staff uniforms",
          CreatedAt = DateTime.UtcNow.AddDays(-15)
        },

        // Items for overdue invoice
        new InvoiceItem
        {
          InvoiceId = overdueInvoice.Id,
          ProductId = products.First(p => p.SKU == "HOME-COFFEE-001").Id,
          Quantity = 1,
          UnitPrice = 89.99m,
          DiscountAmount = 0m,
          Description = "Office coffee maker",
          CreatedAt = DateTime.UtcNow.AddDays(-45)
        },
        new InvoiceItem
        {
          InvoiceId = overdueInvoice.Id,
          ProductId = products.First(p => p.SKU == "SPORT-YOGA-001").Id,
          Quantity = 2,
          UnitPrice = 29.99m,
          DiscountAmount = 0m,
          Description = "Employee wellness program",
          CreatedAt = DateTime.UtcNow.AddDays(-45)
        }
      };

      await context.InvoiceItems.AddRangeAsync(invoiceItems);
      await context.SaveChangesAsync();
    }

    private static string HashPassword(string password)
    {
      // Simple hash for demo purposes - in production, use proper password hashing
      using (var sha256 = SHA256.Create())
      {
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + "SALT"));
        return Convert.ToBase64String(hashedBytes);
      }
    }
  }
}