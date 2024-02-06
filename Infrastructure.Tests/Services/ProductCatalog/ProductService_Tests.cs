using Infrastructure.Contexts;
using Infrastructure.Entities.ProductCatalog;
using Infrastructure.Repositories.ProductRepositories;
using Infrastructure.Services.ProductServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Services.ProductCatalog;

public class ProductService_Tests
{
    private readonly ProductDbContext _context = new(new DbContextOptionsBuilder<ProductDbContext>()
              .UseInMemoryDatabase($"{Guid.NewGuid()}")
              .Options);

    [Fact]
    public void AddProduct_Should_AddProduct_then_ReturnTrue()
    {
        //arrange
        var productRepo = new ProductRepository(_context);
        var manufacturerRepo = new ManufacturerRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var productService = new ProductService(categoryRepo, productRepo, manufacturerRepo);


        //act
        var productEntity = new Product
        {
            ArticleNumber = "Test Article Number",
            ProductName = "Test Product",
            Description = "Test Description",
            Price = 100
        };
        var manufacturerEntity = new Manufacturer
        {
            ManufacturerName = "Test Manufacturer"
        };

        var categoryEntity = new Category
        {
            CategoryName = "Test Category"
        };

        manufacturerRepo.Create(manufacturerEntity);
        var result = productService.AddProduct(productEntity, manufacturerEntity, categoryEntity);

        //assert
        Assert.True(result);
    }

    [Fact]
    public void AddProduct_ShouldNot_AddProduct_When_FieldIsNull_then_ReturnFalse()
    {
        //arrange
        var productRepo = new ProductRepository(_context);
        var manufacturerRepo = new ManufacturerRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var productService = new ProductService(categoryRepo, productRepo, manufacturerRepo);
        var productEntity = new Product
        {
            ArticleNumber = "",
            ProductName = "Test Product",
            Description = "Test Description",
            Price = 100
        };
        var manufacturerEntity = new Manufacturer
        {
            ManufacturerName = "Test Manufacturer"
        };

        var categoryEntity = new Category
        {
            CategoryName = ""
        };

        //act
        var result = productService.AddProduct(productEntity, manufacturerEntity, categoryEntity);

        //assert
        Assert.False(result);

    }

    [Fact]
    public void GetProductByArticleNumber_Should_GetProductByArticleNumber_Then_ReturnProduct()
    {
        //arrange
        var productRepo = new ProductRepository(_context);
        var manufacturerRepo = new ManufacturerRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var productService = new ProductService(categoryRepo, productRepo, manufacturerRepo);
        var productEntity = new Product
        {
            ArticleNumber = "Test Article Number",
            ProductName = "Test Product",
            Description = "Test Description",
            Price = 100
        };
        var manufacturerEntity = new Manufacturer
        {
            ManufacturerName = "Test Manufacturer"
        };

        var categoryEntity = new Category
        {
            CategoryName = "Test Category"
        };

        manufacturerRepo.Create(manufacturerEntity);
        productService.AddProduct(productEntity, manufacturerEntity, categoryEntity);

        //act
        var result = productService.GetProductByArticleNumber("Test Article Number");

        //assert
        Assert.NotNull(result);
    }

    [Fact]
    public void GetProductByArticleNumber_Should_ReturnNull_When_ProductNotFound()
    {
        //arrange
        var productRepo = new ProductRepository(_context);
        var manufacturerRepo = new ManufacturerRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var productService = new ProductService(categoryRepo, productRepo, manufacturerRepo);


        //act
        var result = productService.GetProductByArticleNumber("Test Article Number");

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAllProducts_Should_GetAllProducts_AsList_Then_ReturnListOfProducts()
    {
        //arrange
        var productRepo = new ProductRepository(_context);
        var manufacturerRepo = new ManufacturerRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var productService = new ProductService(categoryRepo, productRepo, manufacturerRepo);
        var productEntity = new Product
        {
            ArticleNumber = "Test Article Number",
            ProductName = "Test Product",
            Description = "Test Description",
            Price = 100
        };
        var manufacturerEntity = new Manufacturer
        {
            ManufacturerName = "Test Manufacturer"
        };

        var categoryEntity = new Category
        {
            CategoryName = "Test Category"
        };

        manufacturerRepo.Create(manufacturerEntity);
        productService.AddProduct(productEntity, manufacturerEntity, categoryEntity);

        //act
        var result = productService.GetAllProducts();

        //assert
        Assert.NotNull(result);
        Assert.IsType<List<Product>>(result);
    }

    [Fact]
    public void GetProductsByCategory_Should_GetProductsByCategory_Then_ReturnListOfProducts()
    {
        //arrange
        var productRepo = new ProductRepository(_context);
        var manufacturerRepo = new ManufacturerRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var productService = new ProductService(categoryRepo, productRepo, manufacturerRepo);
        var productEntity = new Product
        {
            ArticleNumber = "Test Article Number",
            ProductName = "Test Product",
            Description = "Test Description",
            Price = 100
        };
        var manufacturerEntity = new Manufacturer
        {
            ManufacturerName = "Test Manufacturer"
        };

        var categoryEntity = new Category
        {
            CategoryName = "Test Category"
        };

        manufacturerRepo.Create(manufacturerEntity);
        categoryRepo.Create(categoryEntity);
        productService.AddProduct(productEntity, manufacturerEntity, categoryEntity);

        //act
        var result = productService.GetProductsByCategory("Test Category");

        //assert
        Assert.NotNull(result);
        Assert.IsType<List<Product>>(result);
    }

    [Fact]
    public void GetProductsByCategory_Should_ReturnEmptyList_When_CategoryNotFound()
    {
        //arrange
        var productRepo = new ProductRepository(_context);
        var manufacturerRepo = new ManufacturerRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var productService = new ProductService(categoryRepo, productRepo, manufacturerRepo);

        //act
        var result = productService.GetProductsByCategory("Test Category");

        //assert
        Assert.Empty(result);
        Assert.IsType<List<Product>>(result);
    }

    [Fact]
    public void GetProductsByManufacturer_Should_GetProductsByManufacturer_Then_ReturnListOfProducts()
    {
        //arrange
        var productRepo = new ProductRepository(_context);
        var manufacturerRepo = new ManufacturerRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var productService = new ProductService(categoryRepo, productRepo, manufacturerRepo);
        var productEntity = new Product
        {
            ArticleNumber = "Test Article Number",
            ProductName = "Test Product",
            Description = "Test Description",
            Price = 100
        };
        var manufacturerEntity = new Manufacturer
        {
            ManufacturerName = "Test Manufacturer"
        };

        var categoryEntity = new Category
        {
            CategoryName = "Test Category"
        };

        manufacturerRepo.Create(manufacturerEntity);
        categoryRepo.Create(categoryEntity);
        productService.AddProduct(productEntity, manufacturerEntity, categoryEntity);

        //act
        var result = productService.GetProductsByManufacturer("Test Manufacturer");

        //assert
        Assert.NotNull(result);
        Assert.IsType<List<Product>>(result);
    }

    [Fact]
    public void GetProductsByManufacturer_Should_ReturnEmptyList_When_ManufacturerNotFound()
    {
        //arrange
        var productRepo = new ProductRepository(_context);
        var manufacturerRepo = new ManufacturerRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var productService = new ProductService(categoryRepo, productRepo, manufacturerRepo);

        //act
        var result = productService.GetProductsByManufacturer("Test Manufacturer");

        //assert
        Assert.Empty(result);
        Assert.IsType<List<Product>>(result);
    }

    [Fact]
    public void UpdateProduct_Should_UpdateProduct_Then_ReturnTrue()
    {
        //arrange
        var productRepo = new ProductRepository(_context);
        var manufacturerRepo = new ManufacturerRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var productService = new ProductService(categoryRepo, productRepo, manufacturerRepo);
        var productEntity = new Product
        {
            ArticleNumber = "Test Article Number",
            ProductName = "Test Product",
            Description = "Test Description",
            Price = 100
        };
        var manufacturerEntity = new Manufacturer
        {
            ManufacturerName = "Test Manufacturer"
        };

        var categoryEntity = new Category
        {
            CategoryName = "Test Category"
        };

        manufacturerRepo.Create(manufacturerEntity);
        categoryRepo.Create(categoryEntity);
        productService.AddProduct(productEntity, manufacturerEntity, categoryEntity);


        //act
        var updatedProduct = new Product
        {
            ArticleNumber = "Test Article Number",
            ProductName = "Updated Product",
            Description = "Updated Description",
            Price = 200,
            Category = new Category
            {
                CategoryName = "Updated Category"
            },
            Manufacturer = new Manufacturer
            {
                ManufacturerName = "Updated Manufacturer"
            }
        };
        var result = productService.UpdateProduct(updatedProduct);

        //assert
        Assert.True(result);
    }

    [Fact]
    public void UpdateProduct_ShouldNot_UpdateProduct_Then_ReturnFalse()
    {
        //arrange
        var productRepo = new ProductRepository(_context);
        var manufacturerRepo = new ManufacturerRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var productService = new ProductService(categoryRepo, productRepo, manufacturerRepo);
        var productEntity = new Product
        {
            ArticleNumber = "Test Article Number",
            ProductName = "Test Product",
            Description = "Test Description",
            Price = 100
        };
        var manufacturerEntity = new Manufacturer
        {
            ManufacturerName = "Test Manufacturer"
        };

        var categoryEntity = new Category
        {
            CategoryName = "Test Category"
        };

        manufacturerRepo.Create(manufacturerEntity);
        categoryRepo.Create(categoryEntity);
        productService.AddProduct(productEntity, manufacturerEntity, categoryEntity);

        //act
        var updatedProduct = new Product
        {
            ArticleNumber = "Updated Article Number",
            ProductName = "Updated Product",
            Description = "Updated Description",
            Price = 200,
            Category = new Category
            {
                CategoryName = "Updated Category"
            },
            Manufacturer = new Manufacturer
            {
                ManufacturerName = "Updated Manufacturer"
            }
        };
        var result = productService.UpdateProduct(updatedProduct);

        //assert
        Assert.False(result);
    }

    [Fact]
    public void DeleteProduct_Should_DeleteProductByArticleNumber_Then_ReturnTrue()
    {
        //arrange
        var productRepo = new ProductRepository(_context);
        var manufacturerRepo = new ManufacturerRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var productService = new ProductService(categoryRepo, productRepo, manufacturerRepo);
        var productEntity = new Product
        {
            ArticleNumber = "Test Article Number",
            ProductName = "Test Product",
            Description = "Test Description",
            Price = 100
        };
        var manufacturerEntity = new Manufacturer
        {
            ManufacturerName = "Test Manufacturer"
        };

        var categoryEntity = new Category
        {
            CategoryName = "Test Category"
        };

        manufacturerRepo.Create(manufacturerEntity);
        categoryRepo.Create(categoryEntity);
        productService.AddProduct(productEntity, manufacturerEntity, categoryEntity);

        //act
        var result = productService.DeleteProduct("Test Article Number");

        //assert
        Assert.True(result);
    }

    [Fact]
    public void DeleteProduct_ShouldNot_DeleteProduct_When_ProductNotFound_then_ReturnFalse()
    {
        //arrange
        var productRepo = new ProductRepository(_context);
        var manufacturerRepo = new ManufacturerRepository(_context);
        var categoryRepo = new CategoryRepository(_context);
        var productService = new ProductService(categoryRepo, productRepo, manufacturerRepo);

        //act
        var result = productService.DeleteProduct("Test Article Number");

        //assert
        Assert.False(result);
    }
}
