using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class TaskRepository(TaskDbContext context) : Repo<TaskEntity, TaskDbContext>(context)
{
    private readonly TaskDbContext _context = context;


    public override IEnumerable<TaskEntity> GetAll()
    {
        try
        {
            var result = _context.Set<TaskEntity>().Include(Task => Task.Status).Include(Task => Task.Category).ToList();
            if (result != null)
            {
                return result;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    public override TaskEntity GetOne(Expression<Func<TaskEntity, bool>> predicate)
    {
        try
        {
            var result = _context.Set<TaskEntity>()
                .Include(Task => Task.Status)
                .Include(Task => Task.Category)
                .FirstOrDefault(predicate);
            if (result != null)
            {
                return result;
            }

        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }
}
