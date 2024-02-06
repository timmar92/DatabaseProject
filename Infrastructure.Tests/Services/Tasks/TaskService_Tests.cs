using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Services.Tasks;

public class TaskService_Tests
{
    private readonly TaskDbContext _context = new(new DbContextOptionsBuilder<TaskDbContext>()
               .UseInMemoryDatabase($"{Guid.NewGuid()}")
               .Options);

    [Fact]
    public void CreateTask_Should_CreateTask_then_ReturnTrue()
    {
        //arrange
        var taskRepo = new TaskRepository(_context);
        var statusRepo = new StatusRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var userRepo = new UserRepository(_context);
        var assignmentRepo = new AssignmentRepository(_context);
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

        //act
        var result = taskService.CreateTask(taskEntity, statusEntity, categoryEntity, userEntity.UserId);

        //assert
        Assert.True(result);
    }

    [Fact]
    public void CreateTask_ShouldNot_CreateTask_then_ReturnFalse()
    {
        //arrange
        var taskRepo = new TaskRepository(_context);
        var statusRepo = new StatusRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var userRepo = new UserRepository(_context);
        var assignmentRepo = new AssignmentRepository(_context);
        var taskService = new TaskService(assignmentRepo, categoryRepo, statusRepo, taskRepo, userRepo);
        var taskEntity = new TaskEntity();
        var statusEntity = new StatusEntity();
        var categoryEntity = new CategoryEntity();
        var userEntity = new UserEntity();

        //act
        var result = taskService.CreateTask(taskEntity, statusEntity, categoryEntity, userEntity.UserId);

        //assert
        Assert.False(result);
    }

    [Fact]
    public void GetTaskByName_Should_ReturnTaskByName()
    {
        //arrange
        var taskRepo = new TaskRepository(_context);
        var statusRepo = new StatusRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var userRepo = new UserRepository(_context);
        var assignmentRepo = new AssignmentRepository(_context);
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
        var result = taskService.GetTaskByName(taskEntity.TaskName);

        //assert
        Assert.NotNull(result);
        Assert.Equal(taskEntity.TaskName, result.TaskName);
        Assert.Equal(taskEntity.Description, result.Description);
    }

    [Fact]
    public void GetTaskByName_ShouldReturnNull_If_TaskNotExist()
    {
        //arrange
        var taskRepo = new TaskRepository(_context);
        var statusRepo = new StatusRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var userRepo = new UserRepository(_context);
        var assignmentRepo = new AssignmentRepository(_context);
        var taskService = new TaskService(assignmentRepo, categoryRepo, statusRepo, taskRepo, userRepo);


        //act
        var result = taskService.GetTaskByName("NonExistentTask");

        //assert
        Assert.Null(result);

    }

    [Fact]
    public void UpdateTask_Should_UpdateTask_Then_ReturnTrue()
    {
        //arrange
        var taskRepo = new TaskRepository(_context);
        var statusRepo = new StatusRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var userRepo = new UserRepository(_context);
        var assignmentRepo = new AssignmentRepository(_context);
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
        taskEntity.TaskName = "Updated Task";
        taskEntity.Description = "Updated Description";
        taskEntity.Category.CategoryName = "Updated Category";
        taskEntity.Status.StatusName = "Updated Status";
        var result = taskService.UpdateTask(taskEntity);

        //assert
        Assert.True(result);

    }

    [Fact]
    public void UpdateTask_ShouldNot_UpdateTask_Then_ReturnFalse()
    {
        //arrange
        var taskRepo = new TaskRepository(_context);
        var statusRepo = new StatusRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var userRepo = new UserRepository(_context);
        var assignmentRepo = new AssignmentRepository(_context);
        var taskService = new TaskService(assignmentRepo, categoryRepo, statusRepo, taskRepo, userRepo);
        var taskEntity = new TaskEntity();
        var statusEntity = new StatusEntity();
        var categoryEntity = new CategoryEntity();
        var userEntity = new UserEntity();

        //act
        var result = taskService.UpdateTask(taskEntity);

        //assert
        Assert.False(result);
    }

    [Fact]
    public void DeleteTask_Should_DeleteTaskByName_Then_ReturnTrue()
    {
        //arrange
        var taskRepo = new TaskRepository(_context);
        var statusRepo = new StatusRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var userRepo = new UserRepository(_context);
        var assignmentRepo = new AssignmentRepository(_context);
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
        var result = taskService.DeleteTask(taskEntity.TaskName);

        //assert
        Assert.True(result);
    }

    [Fact]
    public void DeleteTask_ShouldNot_DeleteTaskByName_Then_ReturnFalse()
    {
        //arrange
        var taskRepo = new TaskRepository(_context);
        var statusRepo = new StatusRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var userRepo = new UserRepository(_context);
        var assignmentRepo = new AssignmentRepository(_context);
        var taskService = new TaskService(assignmentRepo, categoryRepo, statusRepo, taskRepo, userRepo);
        var taskEntity = new TaskEntity();
        var statusEntity = new StatusEntity();
        var categoryEntity = new CategoryEntity();
        var userEntity = new UserEntity();

        //act
        var result = taskService.DeleteTask("NonExistentTask");

        //assert
        Assert.False(result);
    }

}
