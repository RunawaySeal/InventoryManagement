using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data.Context;
using InventoryManagement.Data.Entities;

namespace InventoryManagement.Pages.Products
{
  public class IndexModel : PageModel
  {
    private readonly InventoryManagementDbContext _context;

    public IndexModel(InventoryManagementDbContext context)
    {
      _context = context;
    }

    public IList<Product> Product { get; set; } = default!;

    public async Task OnGetAsync()
    {
      Product = await _context.Products.ToListAsync();
    }
  }
}
