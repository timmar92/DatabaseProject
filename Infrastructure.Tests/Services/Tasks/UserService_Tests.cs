using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Services.Tasks;

public class UserService_Tests
{
    private readonly TaskDbContext _context = new(new DbContextOptionsBuilder<TaskDbContext>()
               .UseInMemoryDatabase($"{Guid.NewGuid()}")
               .Options);

    [Fact]
    public void CreateUser_Should_CreateUser_Then_ReturnTrue()
    {
        //arrange
        var userRepo = new UserRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var statusRepo = new StatusRepository(_context);
        var taskRepo = new TaskRepository(_context);
        var assignmentRepo = new AssignmentRepository(_context);

        var userService = new UserService(assignmentRepo, categoryRepo, statusRepo, taskRepo, userRepo);

        var userEntity = new UserEntity
        {
            Name = "Test User",
            Email = "Test@domain.com"
        };

        //act
        var result = userService.CreateUser(userEntity);

        //assert
        Assert.True(result);
    }

    [Fact]
    public void CreateUser_Should_Not_CreateUser_IfUserExists_Then_ReturnFalse()
    {
        //arrange
        var userRepo = new UserRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var statusRepo = new StatusRepository(_context);
        var taskRepo = new TaskRepository(_context);
        var assignmentRepo = new AssignmentRepository(_context);

        var userService = new UserService(assignmentRepo, categoryRepo, statusRepo, taskRepo, userRepo);

        var userEntity = new UserEntity
        {
            Name = "Test User",
            Email = "Test@domain.com"
        };
        userService.CreateUser(userEntity);

        //act
        var result = userService.CreateUser(userEntity);

        //assert
        Assert.False(result);
    }
}
