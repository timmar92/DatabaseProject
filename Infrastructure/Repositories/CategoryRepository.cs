using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class CategoryRepository(TaskDbContext context) : Repo<CategoryEntity, TaskDbContext>(context)
{
    private readonly TaskDbContext _context = context;
}
