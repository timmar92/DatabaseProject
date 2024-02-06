using Infrastructure.Contexts;
using Infrastructure.Entities.ProductCatalog;
using Infrastructure.Repositories.ProductRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories.ProductCatalog;

public class CategoryRepository_Tests
{
    private readonly ProductDbContext _context = new(new DbContextOptionsBuilder<ProductDbContext>()
              .UseInMemoryDatabase($"{Guid.NewGuid()}")
              .Options);

    [Fact]
    public void Create_Should_CreateCategory_then_ReturnCategory()
    {
        //arrange
        var categoryRepo = new CategoryRepository(_context);
        var categoryEntity = new Category
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
    public void Create_ShouldNot_CreateCategory_then_ReturnNull()
    {
        //arrange
        var categoryRepo = new CategoryRepository(_context);
        var categoryEntity = new Category();

        //act
        var result = categoryRepo.Create(categoryEntity);

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAll_Should_GetAllCategories_AsList_Then_ReturnList()
    {
        //arrange
        var categoryRepo = new CategoryRepository(_context);

        //act
        var result = categoryRepo.GetAll();

        //assert
        Assert.NotNull(result);
        Assert.IsType<List<Category>>(result);
    }

    [Fact]
    public void GetAll_Should_GetAllCategories_WithPredicate_AsList_Then_ReturnList()
    {
        //arrange
        var categoryRepo = new CategoryRepository(_context);
        var categoryEntity = new Category
        {
            CategoryName = "Test Category",
        };
        categoryRepo.Create(categoryEntity);

        //act
        var result = categoryRepo.GetAll(x => x.CategoryName == "Test Category");

        //assert
        Assert.NotNull(result);
        Assert.IsType<List<Category>>(result);
    }

    [Fact]
    public void GetOne_Should_GetOneCategory_WithPredicate_Then_ReturnCategory()
    {
        //arrange
        var categoryRepo = new CategoryRepository(_context);
        var categoryEntity = new Category
        {
            CategoryName = "Test Category",
        };
        categoryRepo.Create(categoryEntity);

        //act
        var result = categoryRepo.GetOne(x => x.CategoryName == "Test Category");

        //assert
        Assert.NotNull(result);
        Assert.IsType<Category>(result);
    }

    [Fact]
    public void GetOne_ShouldNot_GetOneCategory_IfNotExist_WithPredicate_Then_ReturnNull()
    {
        //arrange
        var categoryRepo = new CategoryRepository(_context);

        //act
        var result = categoryRepo.GetOne(x => x.CategoryName == "Test Category");

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void Delete_Should_DeleteCategory_Then_ReturnTrue()
    {
        //arrange
        var categoryRepo = new CategoryRepository(_context);
        var categoryEntity = new Category
        {
            CategoryName = "Test Category",
        };
        categoryRepo.Create(categoryEntity);

        //act
        var result = categoryRepo.Delete(x => x.CategoryName == "Test Category");

        //assert
        Assert.True(result);
    }

    [Fact]
    public void Delete_ShouldNot_DeleteCategory_IfNotExist_Then_ReturnFalse()
    {
        //arrange
        var categoryRepo = new CategoryRepository(_context);

        //act
        var result = categoryRepo.Delete(x => x.CategoryName == "Test Category");

        //assert
        Assert.False(result);
    }
}
