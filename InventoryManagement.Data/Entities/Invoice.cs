using InventoryManagement.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Data.Entities
{
  public class Invoice
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string InvoiceNumber { get; set; } = string.Empty;

    [Required]
    public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;

    public DateTime? DueDate { get; set; }

    [Required]
    [StringLength(200)]
    public string CustomerName { get; set; } = string.Empty;

    [StringLength(255)]
    public string CustomerEmail { get; set; } = string.Empty;

    [StringLength(20)]
    public string CustomerPhone { get; set; } = string.Empty;

    [StringLength(500)]
    public string CustomerAddress { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;

    [Column(TypeName = "decimal(18,2)")]
    public decimal SubTotal { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TaxAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    [StringLength(1000)]
    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    // Foreign Keys
    [Required]
    public int CreatedByUserId { get; set; }

    // Navigation properties
    [ForeignKey("CreatedByUserId")]
    public virtual User CreatedByUser { get; set; } = null!;

    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

    [NotMapped]
    public bool IsOverdue => DueDate.HasValue && DueDate.Value < DateTime.UtcNow && Status != InvoiceStatus.Paid;

    [NotMapped]
    public int ItemCount => InvoiceItems?.Count ?? 0;
  }
}