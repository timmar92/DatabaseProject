using Infrastructure.Contexts;
using Infrastructure.Entities.ProductCatalog;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.ProductRepositories;

public class CategoryRepository(ProductDbContext context) : Repo<Category, ProductDbContext>(context)
{
    private readonly ProductDbContext _context = context;


}
