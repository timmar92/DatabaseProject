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

    [Fact]
    public void AreThereAnyUsers_Should_ReturnTrue_IfUsersExist()
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
        var result = userService.AreThereAnyUsers();


        //assert
        Assert.True(result);
    }

    [Fact]
    public void AreThereAnyUsers_Should_ReturnFalse_IfNoUsersExist()
    {
        //arrange
        var userRepo = new UserRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var statusRepo = new StatusRepository(_context);
        var taskRepo = new TaskRepository(_context);
        var assignmentRepo = new AssignmentRepository(_context);

        var userService = new UserService(assignmentRepo, categoryRepo, statusRepo, taskRepo, userRepo);

        //act
        var result = userService.AreThereAnyUsers();

        //assert
        Assert.False(result);
    }

    [Fact]
    public void GetUserByEmail_Should_ReturnUser_IfUserExists()
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
        var result = userService.GetUserByEmail(userEntity.Email);

        //assert
        Assert.NotNull(result);
        Assert.Equal(userEntity.Email, result.Email);
    }

    [Fact]
    public void GetUserByEmail_Should_ReturnNull_IfUserDoesNotExist()
    {
        //arrange
        var userRepo = new UserRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var statusRepo = new StatusRepository(_context);
        var taskRepo = new TaskRepository(_context);
        var assignmentRepo = new AssignmentRepository(_context);

        var userService = new UserService(assignmentRepo, categoryRepo, statusRepo, taskRepo, userRepo);

        //act
        var result = userService.GetUserByEmail("");

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void GetIdByEmail_Should_ReturnUserId_IfUserExists()
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
        var result = userService.GetIdByEmail(userEntity.Email);

        //assert
        Assert.NotEqual(0, result);
        Assert.Equal(userEntity.UserId, result);
    }

    [Fact]
    public void GetIdByEmail_Should_ReturnZero_IfUserDoesNotExist()
    {
        //arrange
        var userRepo = new UserRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var statusRepo = new StatusRepository(_context);
        var taskRepo = new TaskRepository(_context);
        var assignmentRepo = new AssignmentRepository(_context);

        var userService = new UserService(assignmentRepo, categoryRepo, statusRepo, taskRepo, userRepo);

        //act
        var result = userService.GetIdByEmail("");

        //assert
        Assert.Equal(0, result);

    }

    [Fact]
    public void GetIdByEmail_Should_ReturnZero_IfUserEmailIsNull()
    {
        //arrange
        var userRepo = new UserRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var statusRepo = new StatusRepository(_context);
        var taskRepo = new TaskRepository(_context);
        var assignmentRepo = new AssignmentRepository(_context);

        var userService = new UserService(assignmentRepo, categoryRepo, statusRepo, taskRepo, userRepo);

        //act
        var result = userService.GetIdByEmail(null!);

        //assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void GetUserById_Should_ReturnUser_IfUserExists()
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
        var result = userService.GetUserById(userEntity.UserId);

        //assert
        Assert.NotNull(result);
        Assert.Equal(userEntity.UserId, result.UserId);

    }

    [Fact]
    public void GetUserById_Should_ReturnNull_IfUserDoesNotExist()
    {
        //arrange
        var userRepo = new UserRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var statusRepo = new StatusRepository(_context);
        var taskRepo = new TaskRepository(_context);
        var assignmentRepo = new AssignmentRepository(_context);

        var userService = new UserService(assignmentRepo, categoryRepo, statusRepo, taskRepo, userRepo);

        //act
        var result = userService.GetUserById(0);

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAllUsers_Should_GetAllUsers_AsList_Then_ReturnList()
    {
        //arrange
        var userRepo = new UserRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var statusRepo = new StatusRepository(_context);
        var taskRepo = new TaskRepository(_context);
        var assignmentRepo = new AssignmentRepository(_context);

        var userService = new UserService(assignmentRepo, categoryRepo, statusRepo, taskRepo, userRepo);

        //act
        var result = userService.GetAllUsers();

        //assert
        Assert.NotNull(result);
        Assert.IsType<List<UserEntity>>(result);
    }

    [Fact]
    public void GetAllUsers_Should_Not_GetAllUsers_IfNoUsersExist_Then_ReturnEmptyList()
    {
        //arrange
        var userRepo = new UserRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var statusRepo = new StatusRepository(_context);
        var taskRepo = new TaskRepository(_context);
        var assignmentRepo = new AssignmentRepository(_context);

        var userService = new UserService(assignmentRepo, categoryRepo, statusRepo, taskRepo, userRepo);

        //act
        var result = userService.GetAllUsers();

        //assert
        Assert.Empty(result);

    }

    [Fact]
    public void UpdateUser_Should_UpdateUser_Then_ReturnTrue()
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
        userEntity.Name = "Updated User";
        userEntity.Email = "Updated@domain.com";
        var result = userService.UpdateUser(userEntity);

        //assert
        Assert.True(result);

    }

    [Fact]
    public void UpdateUser_Should_Not_UpdateUser_IfUserDoesNotExist_Then_ReturnFalse()
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
        userEntity.UserId = 0;
        var result = userService.UpdateUser(userEntity);


        //assert
        Assert.False(result);
    }

    [Fact]
    public void DeleteUser_Should_DeleteUserByEmail_IfUserExists_Then_ReturnTrue()
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
        var result = userService.DeleteUser(userEntity.Email);


        //assert
        Assert.True(result);

    }

    [Fact]
    public void DeleteUser_Should_Not_DeleteUserByEmail_IfUserDoesNotExist_Then_ReturnFalse()
    {
        //arrange
        var userRepo = new UserRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var statusRepo = new StatusRepository(_context);
        var taskRepo = new TaskRepository(_context);
        var assignmentRepo = new AssignmentRepository(_context);

        var userService = new UserService(assignmentRepo, categoryRepo, statusRepo, taskRepo, userRepo);

        //act
        var result = userService.DeleteUser("");

        //assert
        Assert.False(result);
    }
}
