using Infrastructure.Contexts;
using Infrastructure.Entities.ProductCatalog;
using Infrastructure.Repositories.ProductRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories.ProductCatalog;

public class CustomerRepository_Tests
{
    private readonly ProductDbContext _context = new(new DbContextOptionsBuilder<ProductDbContext>()
                  .UseInMemoryDatabase($"{Guid.NewGuid()}")
                  .Options);

    [Fact]
    public void Create_Should_CreateCustomer_then_ReturnCustomer()
    {
        //arrange
        var customerRepo = new CustomerRepository(_context);
        var customerEntity = new Customer
        {
            FirstName = "Test Customer",
            LastName = "Test Customer",
            Email = "test@domain.com"
        };

        //act
        var result = customerRepo.Create(customerEntity);

        //assert
        Assert.NotNull(result);
        Assert.NotEqual(0, result.CustomerId);
        Assert.Equal(customerEntity.FirstName, result.FirstName);
        Assert.Equal(customerEntity.LastName, result.LastName);
        Assert.Equal(customerEntity.Email, result.Email);
    }

    [Fact]
    public void Create_ShouldNot_CreateCustomer_then_ReturnNull()
    {
        //arrange
        var customerRepo = new CustomerRepository(_context);
        var customerEntity = new Customer();

        //act
        var result = customerRepo.Create(customerEntity);

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAll_Should_GetAllCustomers_AsList_Then_ReturnList()
    {
        //arrange
        var customerRepo = new CustomerRepository(_context);

        //act
        var result = customerRepo.GetAll();

        //assert
        Assert.NotNull(result);
        Assert.IsType<List<Customer>>(result);
    }

    [Fact]
    public void GetAll_Should_GetAllCustomers_WithPredicate_AsList_Then_ReturnList()
    {
        //arrange
        var customerRepo = new CustomerRepository(_context);
        var customerEntity = new Customer
        {
            FirstName = "Test Customer",
            LastName = "Test Customer",
            Email = "test@domain.com"
        };
        customerRepo.Create(customerEntity);

        //act
        var result = customerRepo.GetAll(x => x.FirstName == "Test Customer");

        //assert
        Assert.NotNull(result);
        Assert.IsType<List<Customer>>(result);

    }

    [Fact]
    public void GetOne_Should_GetOneCustomer_WithPredicate_Then_ReturnCustomer()
    {
        //arrange
        var customerRepo = new CustomerRepository(_context);
        var customerEntity = new Customer
        {
            FirstName = "Test Customer",
            LastName = "Test Customer",
            Email = "test@domain.com"
        };
        customerRepo.Create(customerEntity);

        //act
        var result = customerRepo.GetOne(x => x.FirstName == "Test Customer");

        //assert
        Assert.NotNull(result);
        Assert.IsType<Customer>(result);
        Assert.Equal(customerEntity.FirstName, result.FirstName);
    }

    [Fact]
    public void GetOne_ShouldNot_GetOneCustomer_IfNotExist_WithPredicate_Then_ReturnNull()
    {
        //arrange
        var customerRepo = new CustomerRepository(_context);


        //act
        var result = customerRepo.GetOne(x => x.FirstName == "Test Customer");

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void Update_Should_UpdateCustomer_Then_ReturnCustomer()
    {
        //arrange
        var customerRepo = new CustomerRepository(_context);
        var customerEntity = new Customer
        {
            FirstName = "Test Customer",
            LastName = "Test Customer",
            Email = "test@domain.com"
        };
        customerRepo.Create(customerEntity);

        //act
        customerEntity.FirstName = "Updated Customer";
        customerEntity.LastName = "Updated Customer";
        customerEntity.Email = "updated@domain.com";

        var result = customerRepo.Update(customerEntity);

        //assert
        Assert.NotNull(result);
        Assert.Equal(customerEntity.FirstName, result.FirstName);
        Assert.Equal(customerEntity.LastName, result.LastName);
        Assert.Equal(customerEntity.Email, result.Email);

    }

    [Fact]
    public void Update_ShouldNot_UpdateCustomer_IfNotExist_Then_ReturnNull()
    {
        //arrange
        var customerRepo = new CustomerRepository(_context);


        //act
        var result = customerRepo.Update(new Customer());

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void Delete_Should_DeleteCustomer_Then_ReturnTrue()
    {
        //arrange
        var customerRepo = new CustomerRepository(_context);
        var customerEntity = new Customer
        {
            FirstName = "Test Customer",
            LastName = "Test Customer",
            Email = "test@domain.com"
        };
        customerRepo.Create(customerEntity);

        //act
        var result = customerRepo.Delete(x => x.FirstName == "Test Customer");

        //assert
        Assert.True(result);
    }

    [Fact]
    public void Delete_ShouldNot_DeleteCustomer_IfNotExist_Then_ReturnFalse()
    {
        //arrange
        var customerRepo = new CustomerRepository(_context);

        //act
        var result = customerRepo.Delete(x => x.FirstName == "Test Customer");

        //assert
        Assert.False(result);
    }
}
