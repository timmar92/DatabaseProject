using Infrastructure.Contexts;
using Infrastructure.Entities.ProductCatalog;
using Infrastructure.Repositories.ProductRepositories;
using Infrastructure.Services.ProductServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Services.ProductCatalog;

public class CategoryService_Tests
{
    private readonly ProductDbContext _context = new(new DbContextOptionsBuilder<ProductDbContext>()
          .UseInMemoryDatabase($"{Guid.NewGuid()}")
          .Options);

    [Fact]
    public void GetCategoryByName_Should_ReturnCategoryByName_if_Exist()
    {
        // Arrange
        var categoryRepo = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepo);


        // Act
        categoryRepo.Create(new Category { CategoryName = "Test Category" });
        var result = categoryService.GetCategoryByName("Test Category");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Category", result.CategoryName);
    }

    [Fact]
    public void GetCategoryByName_Should_ReturnNull_If_NotExist()
    {
        // Arrange
        var categoryRepo = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepo);

        // Act
        var result = categoryService.GetCategoryByName("Test Category");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void DeleteCategory_Should_ReturnTrue_If_CategoryExist()
    {
        // Arrange
        var categoryRepo = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepo);
        categoryRepo.Create(new Category { CategoryName = "Test Category" });

        // Act
        var result = categoryService.DeleteCategory("Test Category");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void DeleteCategory_Should_ReturnFalse_If_CategoryNotExist()
    {
        // Arrange
        var categoryRepo = new CategoryRepository(_context);
        var categoryService = new CategoryService(categoryRepo);

        // Act
        var result = categoryService.DeleteCategory("Test Category");

        // Assert
        Assert.False(result);
    }
}
