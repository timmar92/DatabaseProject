using Infrastructure.Contexts;
using Infrastructure.Entities.ProductCatalog;
using Infrastructure.Repositories.ProductRepositories;
using Infrastructure.Services.ProductServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Services.ProductCatalog;

public class ManufacturerService_Tests
{
    private readonly ProductDbContext _context = new(new DbContextOptionsBuilder<ProductDbContext>()
          .UseInMemoryDatabase($"{Guid.NewGuid()}")
          .Options);

    [Fact]
    public void GetManufacturerByName_Should_GetManufacturerByName_Then_ReturnManufacturer()
    {
        // Arrange
        var manufacturerRepo = new ManufacturerRepository(_context);
        var manufacturerService = new ManufacturerService(manufacturerRepo);

        // Act
        var manufacturer = new Manufacturer
        {
            ManufacturerName = "Test Manufacturer"
        };
        manufacturerRepo.Create(manufacturer);

        var result = manufacturerService.GetManufacturerByName("Test Manufacturer");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Manufacturer", result.ManufacturerName);

    }

    [Fact]
    public void GetManufacturerByName_Should_ReturnNull_When_ManufacturerDoesNotExist()
    {
        // Arrange
        var manufacturerRepo = new ManufacturerRepository(_context);
        var manufacturerService = new ManufacturerService(manufacturerRepo);

        // Act
        var result = manufacturerService.GetManufacturerByName("Test Manufacturer");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void DeleteManufacturer_Should_DeleteManufacturerByName_Then_ReturnTrue()
    {
        // Arrange
        var manufacturerRepo = new ManufacturerRepository(_context);
        var manufacturerService = new ManufacturerService(manufacturerRepo);

        // Act
        var manufacturer = new Manufacturer
        {
            ManufacturerName = "Test Manufacturer"
        };
        manufacturerRepo.Create(manufacturer);

        var result = manufacturerService.DeleteManufacturer("Test Manufacturer");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void DeleteManufacturer_Should_ReturnFalse_When_ManufacturerDoesNotExist()
    {
        // Arrange
        var manufacturerRepo = new ManufacturerRepository(_context);
        var manufacturerService = new ManufacturerService(manufacturerRepo);

        // Act
        var result = manufacturerService.DeleteManufacturer("Test Manufacturer");

        // Assert
        Assert.False(result);
    }
}
