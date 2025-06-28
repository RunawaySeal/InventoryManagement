using InventoryManagement.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Data.Entities
{
  public class Product
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string SKU { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Cost { get; set; }

    [Required]
    public int StockQuantity { get; set; }

    public int MinimumStockLevel { get; set; } = 0;

    [StringLength(100)]
    public ProductCategory Category { get; set; } = ProductCategory.Other;

    [StringLength(100)]
    public string Brand { get; set; } = string.Empty;

    [StringLength(50)]
    public ProductUnit Unit { get; set; } = ProductUnit.Piece;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

    [NotMapped]
    public bool IsLowStock => StockQuantity <= MinimumStockLevel;

    [NotMapped]
    public decimal ProfitMargin => Price > 0 ? ((Price - Cost) / Price) * 100 : 0;
  }
}