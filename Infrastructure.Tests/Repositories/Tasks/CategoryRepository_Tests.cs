using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories.Tasks;

public class CategoryRepository_Tests
{
    private readonly TaskDbContext _context = new(new DbContextOptionsBuilder<TaskDbContext>()
           .UseInMemoryDatabase($"{Guid.NewGuid()}")
           .Options);

    [Fact]
    public void CreateCategory_Should_CreateCategory_then_ReturnCategory()
    {
        //arrange
        var categoryRepo = new CategoryRepository(_context);
        var categoryEntity = new CategoryEntity
        {
            CategoryName = "Test Category",
        };

        //act
        var result = categoryRepo.Create(categoryEntity);

        //assert
        Assert.NotNull(result);
        Assert.NotEqual(0, result.CategoryId);
        Assert.Equal(categoryEntity.CategoryName, result.CategoryName);
    }

    [Fact]
    public void CreateCategory_ShouldNot_CreateCategory_then_ReturnNull()
    {
        //arrange
        var categoryRepo = new CategoryRepository(_context);
        var categoryEntity = new CategoryEntity();

        //act
        var result = categoryRepo.Create(categoryEntity);

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAllCategories_Should_GetAllCategories_AsList_Then_ReturnList()
    {
        //arrange
        var categoryRepo = new CategoryRepository(_context);

        //act
        var result = categoryRepo.GetAll();

        //assert
        Assert.NotNull(result);
        Assert.IsType<List<CategoryEntity>>(result);
    }

    [Fact]
    public void GetAllCategories_Should_GetAllCategories_WithPredicate_AsList_Then_ReturnList()
    {
        //arrange
        var categoryRepo = new CategoryRepository(_context);
        var categoryEntity = new CategoryEntity
        {
            CategoryName = "Test Category",
        };
        categoryRepo.Create(categoryEntity);

        //act
        var result = categoryRepo.GetAll(c => c.CategoryName == "Test Category");

        //assert
        Assert.NotNull(result);
        Assert.IsType<List<CategoryEntity>>(result);
        Assert.Single(result);
    }

    [Fact]
    public void GetOneCategory_Should_GetOneCategory_WithPredicate_Then_ReturnCategory()
    {
        //arrange
        var categoryRepo = new CategoryRepository(_context);
        var categoryEntity = new CategoryEntity
        {
            CategoryName = "Test Category",
        };
        categoryRepo.Create(categoryEntity);

        //act
        var result = categoryRepo.GetOne(c => c.CategoryName == "Test Category");

        //assert
        Assert.NotNull(result);
        Assert.Equal(categoryEntity.CategoryName, result.CategoryName);
    }

    [Fact]
    public void GetOneCategory_ShouldNot_GetOneCategory_WithPredicate_Then_ReturnNull()
    {
        //arrange
        var categoryRepo = new CategoryRepository(_context);
        var categoryEntity = new CategoryEntity
        {
            CategoryName = "Test Category",
        };
        categoryRepo.Create(categoryEntity);

        //act
        var result = categoryRepo.GetOne(c => c.CategoryName == "Test Category 2");

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void UpdateCategory_Should_UpdateCategory_Then_ReturnCategory()
    {
        //arrange
        var categoryRepo = new CategoryRepository(_context);
        var categoryEntity = new CategoryEntity
        {
            CategoryName = "Test Category",
        };
        categoryEntity = categoryRepo.Create(categoryEntity);

        //act
        categoryEntity.CategoryName = "Updated Category";
        categoryRepo.Update(categoryEntity);

        var result = categoryRepo.GetOne(c => c.CategoryId == categoryEntity.CategoryId);

        //assert
        Assert.NotNull(result);
        Assert.Equal(categoryEntity.CategoryName, result.CategoryName);
    }

    [Fact]
    public void UpdateCategory_ShouldNot_UpdateCategory_Then_ReturnNull()
    {
        //arrange
        var categoryRepo = new CategoryRepository(_context);
        var categoryEntity = new CategoryEntity
        {
            CategoryName = "Test Category",
        };
        categoryEntity = categoryRepo.Create(categoryEntity);

        //act
        categoryEntity.CategoryName = "Updated Category";
        categoryRepo.Update(categoryEntity);

        var result = categoryRepo.GetOne(c => c.CategoryName == "Test Category");

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void DeleteCategory_Should_DeleteCategory_Then_ReturnTrue()
    {
        //arrange
        var categoryRepo = new CategoryRepository(_context);
        var categoryEntity = new CategoryEntity
        {
            CategoryName = "Test Category",
        };
        categoryRepo.Create(categoryEntity);

        //act
        var result = categoryRepo.Delete(x => x.CategoryName == categoryEntity.CategoryName);

        //assert
        Assert.True(result);
    }

    [Fact]
    public void DeleteCategory_ShouldNot_DeleteCategory_Then_ReturnFalse()
    {
        //arrange
        var categoryRepo = new CategoryRepository(_context);
        var categoryEntity = new CategoryEntity
        {
            CategoryName = "Test Category",
        };
        categoryRepo.Create(categoryEntity);

        //act
        var result = categoryRepo.Delete(x => x.CategoryName == "Invalid Category");

        //assert
        Assert.False(result);
    }


}
