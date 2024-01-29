using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class StatusRepository(TaskDbContext context) : Repo<StatusEntity, TaskDbContext>(context)
{
    private readonly TaskDbContext _context = context;
}
