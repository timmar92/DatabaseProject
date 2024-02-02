using Infrastructure.Contexts;
using Infrastructure.Entities.ProductCatalog;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Repositories.ProductRepositories;

public class PurchaseRepository(ProductDbContext context) : Repo<Purchase, ProductDbContext>(context)
{
    private readonly ProductDbContext _context = context;

    public override IEnumerable<Purchase> GetAll()
    {
        try
        {
            var result = _context.Set<Purchase>()
                .Include(x => x.Customer)
                .Include(x => x.ProductArticleNumberNavigation)
                .ToList();
            if (result != null)
            {
                return result;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }
}
