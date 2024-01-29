using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class UserRepository(TaskDbContext context) : Repo<UserEntity, TaskDbContext>(context)
{
    private readonly TaskDbContext _context = context;
}
