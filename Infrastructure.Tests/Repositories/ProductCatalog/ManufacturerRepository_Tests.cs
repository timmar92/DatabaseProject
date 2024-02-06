using Infrastructure.Contexts;
using Infrastructure.Entities.ProductCatalog;
using Infrastructure.Repositories.ProductRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories.ProductCatalog;

public class ManufacturerRepository_Tests
{
    private readonly ProductDbContext _context = new(new DbContextOptionsBuilder<ProductDbContext>()
                  .UseInMemoryDatabase($"{Guid.NewGuid()}")
                  .Options);

    [Fact]
    public void Create_Should_CreateManufacturer_then_ReturnManufacturer()
    {
        //arrange
        var manufacturerRepo = new ManufacturerRepository(_context);
        var manufacturerEntity = new Manufacturer
        {
            ManufacturerName = "Test Manufacturer",
        };

        //act
        var result = manufacturerRepo.Create(manufacturerEntity);

        //assert
        Assert.NotNull(result);
        Assert.NotEqual(0, result.ManufacturerId);
        Assert.Equal(manufacturerEntity.ManufacturerName, result.ManufacturerName);
    }

    [Fact]
    public void Create_ShouldNot_CreateManufacturer_then_ReturnNull()
    {
        //arrange
        var manufacturerRepo = new ManufacturerRepository(_context);
        var manufacturerEntity = new Manufacturer();

        //act
        var result = manufacturerRepo.Create(manufacturerEntity);

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAll_Should_GetAllManufacturers_AsList_Then_ReturnList()
    {
        //arrange
        var manufacturerRepo = new ManufacturerRepository(_context);

        //act
        var result = manufacturerRepo.GetAll();

        //assert
        Assert.NotNull(result);
        Assert.IsType<List<Manufacturer>>(result);
    }

    [Fact]
    public void GetAll_Should_GetAllManufacturers_WithPredicate_Then_ReturnList()
    {
        //arrange
        var manufacturerRepo = new ManufacturerRepository(_context);
        var manufacturerEntity = new Manufacturer
        {
            ManufacturerName = "Test Manufacturer",
        };
        manufacturerRepo.Create(manufacturerEntity);

        //act
        var result = manufacturerRepo.GetAll(x => x.ManufacturerName == manufacturerEntity.ManufacturerName);

        //assert
        Assert.NotNull(result);
        Assert.IsType<List<Manufacturer>>(result);
    }

    [Fact]
    public void GetOne_Should_GetOneManufacturer_Then_ReturnManufacturer()
    {
        //arrange
        var manufacturerRepo = new ManufacturerRepository(_context);
        var manufacturerEntity = new Manufacturer
        {
            ManufacturerName = "Test Manufacturer",
        };
        manufacturerRepo.Create(manufacturerEntity);

        //act
        var result = manufacturerRepo.GetOne(x => x.ManufacturerName == manufacturerEntity.ManufacturerName);

        //assert
        Assert.NotNull(result);
        Assert.Equal(manufacturerEntity.ManufacturerName, result.ManufacturerName);
    }

    [Fact]
    public void GetOne_ShouldNot_GetOneManufacturer_Then_ReturnNull()
    {
        //arrange
        var manufacturerRepo = new ManufacturerRepository(_context);

        //act
        var result = manufacturerRepo.GetOne(x => x.ManufacturerName == "Test Manufacturer");

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void Update_Should_UpdateManufacturer_Then_ReturnManufacturer()
    {
        //arrange
        var manufacturerRepo = new ManufacturerRepository(_context);
        var manufacturerEntity = new Manufacturer
        {
            ManufacturerName = "Test Manufacturer",
        };
        manufacturerRepo.Create(manufacturerEntity);

        //act
        manufacturerEntity.ManufacturerName = "Updated Manufacturer";
        var result = manufacturerRepo.Update(manufacturerEntity);

        //assert
        Assert.NotNull(result);
    }

    [Fact]
    public void Update_ShouldNot_UpdateManufacturer_IfNotExist_Then_ReturnNull()
    {
        //arrange
        var manufacturerRepo = new ManufacturerRepository(_context);


        //act
        var result = manufacturerRepo.Update(new Manufacturer());

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void Delete_Should_DeleteManufacturer_Then_ReturnTrue()
    {
        //arrange
        var manufacturerRepo = new ManufacturerRepository(_context);
        var manufacturerEntity = new Manufacturer
        {
            ManufacturerName = "Test Manufacturer",
        };
        manufacturerRepo.Create(manufacturerEntity);

        //act
        var result = manufacturerRepo.Delete(x => x.ManufacturerName == manufacturerEntity.ManufacturerName);

        //assert
        Assert.True(result);
    }

    [Fact]
    public void Delete_ShouldNot_DeleteManufacturer_IfNotExist_Then_ReturnFalse()
    {
        //arrange
        var manufacturerRepo = new ManufacturerRepository(_context);

        //act
        var result = manufacturerRepo.Delete(x => x.ManufacturerName == "Test Manufacturer");

        //assert
        Assert.False(result);
    }
}
