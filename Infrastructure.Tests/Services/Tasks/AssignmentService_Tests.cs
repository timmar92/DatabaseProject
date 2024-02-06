using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Services.Tasks;

public class AssignmentService_Tests
{
    private readonly TaskDbContext _context = new(new DbContextOptionsBuilder<TaskDbContext>()
           .UseInMemoryDatabase($"{Guid.NewGuid()}")
           .Options);

    [Fact]
    public void GetAssignmentsByUserId_Should_GetAssignmentsByUserId_AsList_Then_ReturnList()
    {
        //arrange
        var assignmentRepo = new AssignmentRepository(_context);
        var userRepo = new UserRepository(_context);
        var taskRepo = new TaskRepository(_context);
        var statusRepo = new StatusRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var assignmentService = new AssignmentService(assignmentRepo, categoryRepo, statusRepo, taskRepo, userRepo);
        var taskService = new TaskService(assignmentRepo, categoryRepo, statusRepo, taskRepo, userRepo);

        var taskEntity = new TaskEntity
        {
            TaskName = "Test Task",
            Description = "Test Description"
        };
        var statusEntity = new StatusEntity
        {
            StatusName = "Test Status"
        };
        var categoryEntity = new CategoryEntity
        {
            CategoryName = "Test Category"
        };
        var userEntity = new UserEntity
        {
            Name = "Test User",
            Email = "Test@domain.com"
        };

        userRepo.Create(userEntity);

        taskService.CreateTask(taskEntity, statusEntity, categoryEntity, userEntity.UserId);

        //act
        var result = assignmentService.GetAssignmentsByUserId(userEntity.UserId);

        //assert
        Assert.NotNull(result);
        Assert.IsType<List<AssignmentEntity>>(result);

    }

    [Fact]
    public void GetAssignmentsByUserId_ShouldNot_GetAssignmentsByUserId_IfUserNotExist_Then_ReturnEmptyList()
    {
        //arrange
        var assignmentRepo = new AssignmentRepository(_context);
        var userRepo = new UserRepository(_context);
        var taskRepo = new TaskRepository(_context);
        var statusRepo = new StatusRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var assignmentService = new AssignmentService(assignmentRepo, categoryRepo, statusRepo, taskRepo, userRepo);
        var taskService = new TaskService(assignmentRepo, categoryRepo, statusRepo, taskRepo, userRepo);

        //act
        var result = assignmentService.GetAssignmentsByUserId(1);

        //assert
        Assert.Empty(result);
        Assert.IsType<List<AssignmentEntity>>(result);
    }

    [Fact]
    public void GetAllAssignments_Should_GetAllAssignments_AsList_Then_ReturnList()
    {
        //arrange
        var assignmentRepo = new AssignmentRepository(_context);
        var userRepo = new UserRepository(_context);
        var taskRepo = new TaskRepository(_context);
        var statusRepo = new StatusRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var assignmentService = new AssignmentService(assignmentRepo, categoryRepo, statusRepo, taskRepo, userRepo);
        var taskService = new TaskService(assignmentRepo, categoryRepo, statusRepo, taskRepo, userRepo);

        //act
        var result = assignmentService.GetAllAssignments();

        //assert
        Assert.NotNull(result);
        Assert.IsType<List<AssignmentEntity>>(result);
    }

    [Fact]
    public void GetAllAssignments_Should_Not_GetAllAssignments_IfNoAssignmentsExist_Then_ReturnEmptyList()
    {
        //arrange
        var assignmentRepo = new AssignmentRepository(_context);
        var userRepo = new UserRepository(_context);
        var taskRepo = new TaskRepository(_context);
        var statusRepo = new StatusRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var assignmentService = new AssignmentService(assignmentRepo, categoryRepo, statusRepo, taskRepo, userRepo);
        var taskService = new TaskService(assignmentRepo, categoryRepo, statusRepo, taskRepo, userRepo);

        //act
        var result = assignmentService.GetAllAssignments();

        //assert
        Assert.Empty(result);
        Assert.IsType<List<AssignmentEntity>>(result);
    }
}
