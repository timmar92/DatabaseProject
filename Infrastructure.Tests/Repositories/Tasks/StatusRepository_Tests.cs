using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories.Tasks;

public class StatusRepository_Tests
{
    private readonly TaskDbContext _context = new(new DbContextOptionsBuilder<TaskDbContext>()
               .UseInMemoryDatabase($"{Guid.NewGuid()}")
               .Options);

    [Fact]
    public void CreateStatus_Should_CreateStatus_then_ReturnStatus()
    {
        //arrange
        var statusRepo = new StatusRepository(_context);
        var statusEntity = new StatusEntity
        {
            StatusName = "Test Status",
        };

        //act
        var result = statusRepo.Create(statusEntity);

        //assert
        Assert.NotNull(result);
        Assert.NotEqual(0, result.StatusId);
        Assert.Equal(statusEntity.StatusName, result.StatusName);
    }

    [Fact]
    public void CreateStatus_ShouldNot_CreateStatus_then_ReturnNull()
    {
        //arrange
        var statusRepo = new StatusRepository(_context);
        var statusEntity = new StatusEntity();

        //act
        var result = statusRepo.Create(statusEntity);

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAllStatus_Should_GetAllStatus_AsList_Then_ReturnList()
    {
        //arrange
        var statusRepo = new StatusRepository(_context);

        //act
        var result = statusRepo.GetAll();

        //assert
        Assert.NotNull(result);
        Assert.IsType<List<StatusEntity>>(result);
    }

    [Fact]
    public void GetAllStatus_Should_GetAllStatus_WithPredicate_AsList_Then_ReturnList()
    {
        //arrange
        var statusRepo = new StatusRepository(_context);

        //act
        var result = statusRepo.GetAll(x => x.StatusName == "Test Status");

        //assert
        Assert.NotNull(result);
        Assert.IsType<List<StatusEntity>>(result);
    }

    [Fact]
    public void GetOneStatus_Should_GetOneStatus_WithPredicate_Then_ReturnStatus()
    {
        //arrange
        var statusRepo = new StatusRepository(_context);
        var statusEntity = new StatusEntity
        {
            StatusName = "Test Status",
        };
        statusRepo.Create(statusEntity);

        //act
        var result = statusRepo.GetOne(x => x.StatusName == statusEntity.StatusName);

        //assert
        Assert.NotNull(result);
        Assert.Equal(statusEntity.StatusName, result.StatusName);
    }

    [Fact]
    public void GetOneStatus_ShouldNot_GetOneStatus_WithPredicate_Then_ReturnNull()
    {
        //arrange
        var statusRepo = new StatusRepository(_context);
        var statusEntity = new StatusEntity
        {
            StatusName = "Test Status",
        };
        statusRepo.Create(statusEntity);

        //act
        var result = statusRepo.GetOne(x => x.StatusName == "Invalid Status");

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void UpdateStatus_Should_UpdateStatus_Then_ReturnStatus()
    {
        //arrange
        var statusRepo = new StatusRepository(_context);
        var statusEntity = new StatusEntity
        {
            StatusName = "Test Status",
        };
        statusEntity = statusRepo.Create(statusEntity);

        //act
        statusEntity.StatusName = "Updated Status";
        var result = statusRepo.Update(statusEntity);

        //assert
        Assert.NotNull(result);
        Assert.Equal(statusEntity.StatusName, result.StatusName);
    }

    [Fact]
    public void DeleteStatus_Should_DeleteStatus_Then_ReturnTrue()
    {
        //arrange
        var statusRepo = new StatusRepository(_context);
        var statusEntity = new StatusEntity
        {
            StatusName = "Test Status",
        };
        statusEntity = statusRepo.Create(statusEntity);

        //act
        var result = statusRepo.Delete(x => x.StatusName == statusEntity.StatusName);

        //assert
        Assert.True(result);
    }


}
