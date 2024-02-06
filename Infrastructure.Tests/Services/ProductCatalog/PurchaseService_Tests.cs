using Infrastructure.Contexts;
using Infrastructure.Entities.ProductCatalog;
using Infrastructure.Repositories.ProductRepositories;
using Infrastructure.Services.ProductServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Services.ProductCatalog;

public class PurchaseService_Tests
{
    private readonly ProductDbContext _context = new(new DbContextOptionsBuilder<ProductDbContext>()
          .UseInMemoryDatabase($"{Guid.NewGuid()}")
          .Options);

    [Fact]
    public void AddPurchase_Should_AddPurchase_Then_ReturnTrue()
    {
        // Arrange
        var purchaseRepo = new PurchaseRepository(_context);
        var productRepo = new ProductRepository(_context);
        var customerRepo = new CustomerRepository(_context);
        var purchaseService = new PurchaseService(productRepo, customerRepo, purchaseRepo);
        var customer = new Customer
        {
            FirstName = "Test",
            LastName = "Test",
            Phone = "1234567890",
            Email = "test@domain.com"
        };
        customerRepo.Create(customer);
        var product = new Product
        {
            ArticleNumber = "123",
            ProductName = "Test",
            Description = "Test",
            Price = 100
        };
        productRepo.Create(product);

        var purchase = new Purchase
        {
            CustomerId = customer.CustomerId,
            ProductArticleNumber = product.ArticleNumber,
            Quantity = 1,
            UnitPrice = product.Price
        };

        // Act
        var result = purchaseService.AddPurchase(purchase, customer, product);

        // Assert
        Assert.True(result);

    }

    [Fact]
    public void AddPurchase_Should_NotAddPurchase_If_CustomerNotFound_Then_ReturnFalse()
    {
        // Arrange
        var purchaseRepo = new PurchaseRepository(_context);
        var productRepo = new ProductRepository(_context);
        var customerRepo = new CustomerRepository(_context);
        var purchaseService = new PurchaseService(productRepo, customerRepo, purchaseRepo);
        var customer = new Customer
        {
            FirstName = "Test",
            LastName = "Test",
            Phone = "1234567890",
            Email = "test@domain.com"
        };

        var product = new Product
        {
            ArticleNumber = "123",
            ProductName = "Test",
            Description = "Test",
            Price = 100
        };
        productRepo.Create(product);

        var purchase = new Purchase
        {
            CustomerId = customer.CustomerId,
            ProductArticleNumber = product.ArticleNumber,
            Quantity = 1,
            UnitPrice = product.Price
        };

        // Act
        var result = purchaseService.AddPurchase(purchase, customer, product);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GetPurchasesbyCustomer_Should_ReturnPurchases_If_CustomerFound()
    {
        // Arrange
        var purchaseRepo = new PurchaseRepository(_context);
        var productRepo = new ProductRepository(_context);
        var customerRepo = new CustomerRepository(_context);
        var purchaseService = new PurchaseService(productRepo, customerRepo, purchaseRepo);
        var customer = new Customer
        {
            FirstName = "Test",
            LastName = "Test",
            Phone = "1234567890",
            Email = "test@domain.com"
        };
        customerRepo.Create(customer);

        var product = new Product
        {
            ArticleNumber = "123",
            ProductName = "Test",
            Description = "Test",
            Price = 100
        };
        productRepo.Create(product);

        var purchase = new Purchase
        {
            CustomerId = customer.CustomerId,
            ProductArticleNumber = product.ArticleNumber,
            Quantity = 1,
            UnitPrice = product.Price
        };
        purchaseRepo.Create(purchase);

        // Act
        var result = purchaseService.GetPurchasesbyCustomer(customer.CustomerId);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.IsType<List<Purchase>>(result);
    }

    [Fact]
    public void GetPurchasesbyCustomer_Should_ReturnEmptyList_If_CustomerNotFound()
    {
        // Arrange
        var purchaseRepo = new PurchaseRepository(_context);
        var productRepo = new ProductRepository(_context);
        var customerRepo = new CustomerRepository(_context);
        var purchaseService = new PurchaseService(productRepo, customerRepo, purchaseRepo);
        var customer = new Customer
        {
            FirstName = "Test",
            LastName = "Test",
            Phone = "1234567890",
            Email = "test@domain.com"
        };

        var product = new Product
        {
            ArticleNumber = "123",
            ProductName = "Test",
            Description = "Test",
            Price = 100
        };
        productRepo.Create(product);

        var purchase = new Purchase
        {
            CustomerId = customer.CustomerId,
            ProductArticleNumber = product.ArticleNumber,
            Quantity = 1,
            UnitPrice = product.Price
        };
        purchaseRepo.Create(purchase);

        // Act
        var result = purchaseService.GetPurchasesbyCustomer(customer.CustomerId);

        // Assert
        Assert.Empty(result);
        Assert.IsType<List<Purchase>>(result);
    }
}
