﻿using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Repositories;

public class AssignmentRepository(TaskDbContext context) : Repo<AssignmentEntity, TaskDbContext>(context)
{
    private readonly TaskDbContext _context = context;

    public override IEnumerable<AssignmentEntity> GetAll()
    {
        try
        {
            var result = _context.Set<AssignmentEntity>()
                .Include(assignment => assignment.User)
                .Include(assignment => assignment.Task).ThenInclude(task => task.Status)
                .Include(assignment => assignment.Task).ThenInclude(task => task.Category)
                .ToList();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
    }
}