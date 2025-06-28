using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data.Context;
using InventoryManagement.Data.Seeders;

namespace InventoryManagement.Extensions
{
  public static class MigrationExtensions
  {
    /// <summary>
    /// Applies any pending migrations and seeds the database
    /// </summary>
    public static async Task<IServiceProvider> MigrateAndSeedDatabaseAsync(this IServiceProvider serviceProvider)
    {
      using var scope = serviceProvider.CreateScope();
      var context = scope.ServiceProvider.GetRequiredService<InventoryManagementDbContext>();
      var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

      try
      {
        // Apply any pending migrations
        if (context.Database.GetPendingMigrations().Any())
        {
          logger.LogInformation("Applying pending migrations...");
          await context.Database.MigrateAsync();
          logger.LogInformation("Migrations applied successfully");
        }
        else
        {
          // If no migrations, ensure database is created
          await context.Database.EnsureCreatedAsync();
          logger.LogInformation("Database ensured");
        }

        // Seed the database
        await DbSeeder.SeedAsync(context);
        logger.LogInformation("Database seeded successfully");
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while migrating or seeding the database");
        throw;
      }

      return serviceProvider;
    }

    /// <summary>
    /// Recreates the database (useful for development)
    /// </summary>
    public static async Task<IServiceProvider> RecreateAndSeedDatabaseAsync(this IServiceProvider serviceProvider)
    {
      using var scope = serviceProvider.CreateScope();
      var context = scope.ServiceProvider.GetRequiredService<InventoryManagementDbContext>();
      var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

      try
      {
        logger.LogWarning("Recreating database - all data will be lost!");

        // Delete and recreate the database
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        logger.LogInformation("Database recreated successfully");

        // Seed the database
        await DbSeeder.SeedAsync(context);
        logger.LogInformation("Database seeded successfully");
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred while recreating or seeding the database");
        throw;
      }

      return serviceProvider;
    }
  }
}