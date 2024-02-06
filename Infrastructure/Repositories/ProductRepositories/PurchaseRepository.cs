using Infrastructure.Contexts;
using Infrastructure.Entities.ProductCatalog;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Repositories.ProductRepositories;

public class PurchaseRepository(ProductDbContext context) : Repo<Purchase, ProductDbContext>(context)
{
    private readonly ProductDbContext _context = context;

    public override Purchase Create(Purchase entity)
    {
        try
        {
            if (entity.ProductArticleNumber != null && entity.CustomerId != 0)
            {
                _context.Set<Purchase>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            else
            {
                return null!;
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
    }

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
