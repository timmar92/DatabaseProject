using Infrastructure.Contexts;
using Infrastructure.Entities.ProductCatalog;
using Infrastructure.Repositories.ProductRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories.ProductCatalog;

public class ProductRepository_Tests
{
    private readonly ProductDbContext _context = new(new DbContextOptionsBuilder<ProductDbContext>()
              .UseInMemoryDatabase($"{Guid.NewGuid()}")
              .Options);

    [Fact]
    public void Create_Should_CreateProduct_then_ReturnProduct()
    {
        //arrange
        var productRepo = new ProductRepository(_context);
        var productEntity = new Product
        {
            ArticleNumber = "123456",
            ProductName = "Test Product",
            Description = "Test Product Description",
            Price = 100,
            CategoryId = 1,
            ManufacturerId = 1
        };

        //act
        var result = productRepo.Create(productEntity);

        //assert
        Assert.NotNull(result);
        Assert.Equal(productEntity.ProductName, result.ProductName);
        Assert.Equal(productEntity.Description, result.Description);
        Assert.Equal(productEntity.Price, result.Price);
        Assert.Equal(productEntity.CategoryId, result.CategoryId);
        Assert.Equal(productEntity.ManufacturerId, result.ManufacturerId);
        Assert.Equal(productEntity.ArticleNumber, result.ArticleNumber);
    }

    [Fact]
    public void Create_ShouldNot_CreateProduct_then_ReturnNull()
    {
        //arrange
        var productRepo = new ProductRepository(_context);
        var productEntity = new Product();

        //act
        var result = productRepo.Create(productEntity);

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAll_Should_GetAllProducts_AsList_Then_ReturnList()
    {
        //arrange
        var productRepo = new ProductRepository(_context);

        //act
        var result = productRepo.GetAll();

        //assert
        Assert.NotNull(result);
        Assert.IsType<List<Product>>(result);
    }

    [Fact]
    public void GetOne_Should_GetOneProduct_WithPredicate_Then_ReturnProduct()
    {
        //arrange
        var productRepo = new ProductRepository(_context);
        var productEntity = new Product
        {
            ArticleNumber = "123456",
            ProductName = "Test Product",
            Description = "Test Product Description",
            Price = 100,
            CategoryId = 1,
            ManufacturerId = 1
        };
        productRepo.Create(productEntity);

        //act
        var result = productRepo.GetOne(x => x.ArticleNumber == "123456");

        //assert
        Assert.NotNull(result);
        Assert.Equal(productEntity.ProductName, result.ProductName);
        Assert.Equal(productEntity.Description, result.Description);
        Assert.Equal(productEntity.Price, result.Price);
        Assert.Equal(productEntity.CategoryId, result.CategoryId);
        Assert.Equal(productEntity.ManufacturerId, result.ManufacturerId);
        Assert.Equal(productEntity.ArticleNumber, result.ArticleNumber);
    }

    [Fact]
    public void GetOne_ShouldNot_GetOneProduct_Then_ReturnNull()
    {
        //arrange
        var productRepo = new ProductRepository(_context);
        var productEntity = new Product
        {
            ArticleNumber = "123456",
            ProductName = "Test Product",
            Description = "Test Product Description",
            Price = 100,
            CategoryId = 1,
            ManufacturerId = 1
        };
        productRepo.Create(productEntity);

        //act
        var result = productRepo.GetOne(x => x.ArticleNumber == "654321");

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void Update_Should_UpdateProduct_Then_ReturnProduct()
    {
        //arrange
        var productRepo = new ProductRepository(_context);
        var productEntity = new Product
        {
            ArticleNumber = "123456",
            ProductName = "Test Product",
            Description = "Test Product Description",
            Price = 100,
            CategoryId = 1,
            ManufacturerId = 1
        };
        productEntity = productRepo.Create(productEntity);

        //act
        productEntity.ProductName = "Updated Product";
        productEntity.Description = "Updated Product Description";
        productEntity.Price = 200;
        productEntity.CategoryId = 2;
        productEntity.ManufacturerId = 2;
        productRepo.Update(productEntity);

        var result = productRepo.GetOne(x => x.ArticleNumber == "123456");

        //assert
        Assert.NotNull(result);
        Assert.Equal(productEntity.ProductName, result.ProductName);
        Assert.Equal(productEntity.Description, result.Description);
        Assert.Equal(productEntity.Price, result.Price);
        Assert.Equal(productEntity.CategoryId, result.CategoryId);
        Assert.Equal(productEntity.ManufacturerId, result.ManufacturerId);
        Assert.Equal(productEntity.ArticleNumber, result.ArticleNumber);
    }

    [Fact]
    public void Update_ShouldNot_UpdateProduct_IfNotExist_Then_ReturnNull()
    {
        //arrange
        var productRepo = new ProductRepository(_context);

        //act
        var result = productRepo.Update(new Product());

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void Delete_Should_DeleteProduct_Then_ReturnTrue()
    {
        //arrange
        var productRepo = new ProductRepository(_context);
        var productEntity = new Product
        {
            ArticleNumber = "123456",
            ProductName = "Test Product",
            Description = "Test Product Description",
            Price = 100,
            CategoryId = 1,
            ManufacturerId = 1
        };
        productEntity = productRepo.Create(productEntity);

        //act
        var result = productRepo.Delete(x => x.ArticleNumber == productEntity.ArticleNumber);

        //assert
        Assert.True(result);
    }

    [Fact]
    public void Delete_ShouldNot_DeleteProduct_IfNotExist_Then_ReturnFalse()
    {
        //arrange
        var productRepo = new ProductRepository(_context);

        //act
        var result = productRepo.Delete(x => x.ArticleNumber == "654321");

        //assert
        Assert.False(result);
    }
}
