using Infrastructure.Contexts;
using Infrastructure.Entities.ProductCatalog;

namespace Infrastructure.Repositories.ProductRepositories;

public class CustomerRepository(ProductDbContext context) : Repo<Customer, ProductDbContext>(context)
{
    private readonly ProductDbContext _context = context;
}
