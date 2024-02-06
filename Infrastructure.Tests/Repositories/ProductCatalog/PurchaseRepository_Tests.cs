using Infrastructure.Contexts;
using Infrastructure.Entities.ProductCatalog;
using Infrastructure.Repositories.ProductRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories.ProductCatalog;

public class PurchaseRepository_Tests
{
    private readonly ProductDbContext _context = new(new DbContextOptionsBuilder<ProductDbContext>()
          .UseInMemoryDatabase($"{Guid.NewGuid()}")
          .Options);

    [Fact]
    public void Create_Should_CreatePurchase_then_ReturnPurchase()
    {
        //arrange
        var purchaseRepo = new PurchaseRepository(_context);
        var purchaseEntity = new Purchase
        {
            CustomerId = 1,
            ProductArticleNumber = "123456",
            Quantity = 1,
            UnitPrice = 100,
        };

        //act
        var result = purchaseRepo.Create(purchaseEntity);

        //assert
        Assert.NotNull(result);
        Assert.NotEqual(0, result.PurchaseId);
        Assert.Equal(purchaseEntity.CustomerId, result.CustomerId);
        Assert.Equal(purchaseEntity.ProductArticleNumber, result.ProductArticleNumber);
        Assert.Equal(purchaseEntity.Quantity, result.Quantity);
        Assert.Equal(purchaseEntity.UnitPrice, result.UnitPrice);
    }

    [Fact]
    public void Create_ShouldNot_CreatePurchase_then_ReturnNull()
    {
        //arrange
        var purchaseRepo = new PurchaseRepository(_context);
        var purchaseEntity = new Purchase();

        //act
        var result = purchaseRepo.Create(purchaseEntity);

        //assert
        Assert.Null(result);

    }

    [Fact]
    public void GetAll_Should_GetAllPurchases_AsList_Then_ReturnList()
    {
        //arrange
        var purchaseRepo = new PurchaseRepository(_context);

        //act
        var result = purchaseRepo.GetAll();

        //assert
        Assert.NotNull(result);
        Assert.IsType<List<Purchase>>(result);
    }

    [Fact]
    public void GetOne_Should_GetOnePurchase_WithPredicate_Then_ReturnPurchase()
    {
        //arrange
        var purchaseRepo = new PurchaseRepository(_context);
        var purchaseEntity = new Purchase
        {
            CustomerId = 1,
            ProductArticleNumber = "123456",
            Quantity = 1,
            UnitPrice = 100,
        };
        purchaseRepo.Create(purchaseEntity);

        //act
        var result = purchaseRepo.GetOne(x => x.CustomerId == 1);

        //assert
        Assert.NotNull(result);
        Assert.Equal(purchaseEntity.CustomerId, result.CustomerId);
        Assert.Equal(purchaseEntity.ProductArticleNumber, result.ProductArticleNumber);
        Assert.Equal(purchaseEntity.Quantity, result.Quantity);
        Assert.Equal(purchaseEntity.UnitPrice, result.UnitPrice);
    }

    [Fact]
    public void GetOne_ShouldNot_GetOnePurchase_Then_ReturnNull()
    {
        //arrange
        var purchaseRepo = new PurchaseRepository(_context);

        //act
        var result = purchaseRepo.GetOne(x => x.CustomerId == 1);

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void Update_Should_UpdatePurchase_Then_ReturnUpdatedPurchase()
    {
        //arrange
        var purchaseRepo = new PurchaseRepository(_context);
        var purchaseEntity = new Purchase
        {
            CustomerId = 1,
            ProductArticleNumber = "123456",
            Quantity = 1,
            UnitPrice = 100,
        };
        purchaseRepo.Create(purchaseEntity);


        //act
        purchaseEntity.CustomerId = 2;
        purchaseEntity.UnitPrice = 200;
        purchaseEntity.Quantity = 2;

        var result = purchaseRepo.Update(purchaseEntity);

        //assert
        Assert.NotNull(result);
        Assert.Equal(purchaseEntity.CustomerId, result.CustomerId);
        Assert.Equal(purchaseEntity.ProductArticleNumber, result.ProductArticleNumber);
        Assert.Equal(purchaseEntity.Quantity, result.Quantity);
        Assert.Equal(purchaseEntity.UnitPrice, result.UnitPrice);
    }

    [Fact]
    public void Update_ShouldNot_UpdatePurchase_IfNotExist_Then_ReturnNull()
    {
        //arrange
        var purchaseRepo = new PurchaseRepository(_context);
        var purchaseEntity = purchaseRepo.Create(new Purchase());

        //act
        var result = purchaseRepo.Update(purchaseEntity);

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void Delete_Should_DeletePurchase_Then_ReturnTrue()
    {
        //arrange
        var purchaseRepo = new PurchaseRepository(_context);
        var purchaseEntity = new Purchase
        {
            CustomerId = 1,
            ProductArticleNumber = "123456",
            Quantity = 1,
            UnitPrice = 100,
        };
        purchaseRepo.Create(purchaseEntity);

        //act
        var result = purchaseRepo.Delete(x => x.PurchaseId == purchaseEntity.PurchaseId);

        //assert
        Assert.True(result);
    }

    [Fact]
    public void Delete_ShouldNot_DeletePurchase_IfNotExist_Then_ReturnFalse()
    {
        //arrange
        var purchaseRepo = new PurchaseRepository(_context);

        //act
        var result = purchaseRepo.Delete(x => x.PurchaseId == 1);

        //assert
        Assert.False(result);
    }
}
