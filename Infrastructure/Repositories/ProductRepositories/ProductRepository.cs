using Infrastructure.Contexts;
using Infrastructure.Entities.ProductCatalog;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Repositories.ProductRepositories;

public class ProductRepository(ProductDbContext context) : Repo<Product, ProductDbContext>(context)
{
    private readonly ProductDbContext _context = context;

    public override IEnumerable<Product> GetAll()
    {
        try
        {
            var result = _context.Set<Product>()
                .Include(x => x.Category)
                .Include(x => x.Manufacturer)
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
