using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories.Tasks;

public class TaskRepository_Tests
{
    private readonly TaskDbContext _context = new(new DbContextOptionsBuilder<TaskDbContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);



    [Fact]
    public void CreateTask_Should_CreateTask_then_ReturnTask()
    {
        //arrange
        var taskRepo = new TaskRepository(_context);
        var taskEntity = new TaskEntity
        {
            TaskName = "Test Task",
            Description = "Test Description"
        };


        //act
        var result = taskRepo.Create(taskEntity);


        //assert
        Assert.NotNull(result);
        Assert.NotEqual(0, result.TaskId);
        Assert.Equal(taskEntity.TaskName, result.TaskName);
    }

    [Fact]
    public void CreateTask_ShouldNot_CreateTask_then_ReturnNull()
    {
        //arrange
        var taskRepo = new TaskRepository(_context);
        var taskEntity = new TaskEntity();


        //act
        var result = taskRepo.Create(taskEntity);


        //assert
        Assert.Null(result);
    }


    [Fact]
    public void GetAllTasks_Should_GetAllTasks_AsList_Then_ReturnList()
    {
        //arrange
        var taskRepo = new TaskRepository(_context);


        //act
        var result = taskRepo.GetAll();


        //assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<TaskEntity>>(result);
    }

    [Fact]
    public void GetOneTask_Should_GetOneTaskByName_Then_ReturnTask()
    {
        //arrange
        var taskRepo = new TaskRepository(_context);
        var taskEntity = new TaskEntity
        {
            TaskName = "Test Task",
            Description = "Test Description",
            Category = new CategoryEntity { CategoryName = "Test Category" },
            Status = new StatusEntity { StatusName = "Test Status" }
        };
        taskRepo.Create(taskEntity);



        //act
        var result = taskRepo.GetOne(x => x.TaskName == taskEntity.TaskName);



        //assert
        Assert.NotNull(result);
        Assert.Equal(taskEntity.TaskName, result.TaskName);

    }

    [Fact]
    public void GetOneTask_ShouldNot_GetOneTaskByName_Then_ReturnNull()
    {
        //arrange
        var taskRepo = new TaskRepository(_context);
        var taskEntity = new TaskEntity
        {
            TaskName = "Test Task",
            Description = "Test Description",
            Category = new CategoryEntity { CategoryName = "Test Category" },
            Status = new StatusEntity { StatusName = "Test Status" }
        };


        //act
        var result = taskRepo.GetOne(x => x.TaskName == taskEntity.TaskName);



        //assert
        Assert.Null(result);

    }

    [Fact]
    public void UpdateTask_Should_UpdateTask_Then_ReturnTask()
    {
        //arrange
        var taskRepo = new TaskRepository(_context);
        var taskEntity = new TaskEntity
        {
            TaskName = "Test Task",
            Description = "Test Description",
            Category = new CategoryEntity { CategoryName = "Test Category" },
            Status = new StatusEntity { StatusName = "Test Status" }
        };
        taskEntity = taskRepo.Create(taskEntity);


        //act
        taskEntity.TaskName = "Updated Task";
        taskEntity.Description = "Updated Description";
        taskEntity.Category.CategoryName = "Updated Category";
        taskEntity.Status.StatusName = "Updated Status";
        taskRepo.Update(taskEntity);

        var result = taskRepo.GetOne(x => x.TaskId == taskEntity.TaskId);

        //assert
        Assert.NotNull(result);
        Assert.Equal(taskEntity.TaskName, result.TaskName);
        Assert.Equal(taskEntity.Description, result.Description);
        Assert.Equal(taskEntity.Category.CategoryName, result.Category.CategoryName);
        Assert.Equal(taskEntity.Status.StatusName, result.Status.StatusName);
    }

    [Fact]
    public void DeleteTask_Should_DeleteTaskByName_Then_ReturnTrue()
    {
        //arrange
        var taskRepo = new TaskRepository(_context);
        var taskEntity = new TaskEntity
        {
            TaskName = "Test Task",
            Description = "Test Description",
            Category = new CategoryEntity { CategoryName = "Test Category" },
            Status = new StatusEntity { StatusName = "Test Status" }
        };

        var createdTask = taskRepo.Create(taskEntity);

        //act
        var result = taskRepo.Delete(x => x.TaskName == createdTask.TaskName);


        //assert
        Assert.True(result);
    }

    [Fact]
    public void DeleteTask_ShouldNot_DeleteTaskByName_Then_ReturnFalse()
    {
        //arrange
        var taskRepo = new TaskRepository(_context);
        var taskEntity = new TaskEntity
        {
            TaskName = "Test Task",
            Description = "Test Description",
            Category = new CategoryEntity { CategoryName = "Test Category" },
            Status = new StatusEntity { StatusName = "Test Status" }
        };

        var createdTask = taskRepo.Create(taskEntity);

        //act
        var result = taskRepo.Delete(x => x.TaskName == "Invalid Task");

        //assert
        Assert.False(result);
    }
}
