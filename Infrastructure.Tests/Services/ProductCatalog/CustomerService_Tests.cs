using Infrastructure.Contexts;
using Infrastructure.Entities.ProductCatalog;
using Infrastructure.Repositories.ProductRepositories;
using Infrastructure.Services.ProductServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Services.ProductCatalog;

public class CustomerService_Tests
{
    private readonly ProductDbContext _context = new(new DbContextOptionsBuilder<ProductDbContext>()
          .UseInMemoryDatabase($"{Guid.NewGuid()}")
          .Options);

    [Fact]
    public void AddCustomer_Should_AddCustomer_Then_ReturnTrue()
    {
        // Arrange
        var customerRepo = new CustomerRepository(_context);
        var customerService = new CustomerService(customerRepo);

        // Act
        var customer = new Customer
        {
            FirstName = "Test",
            LastName = "Test",
            Phone = "1234567890",
            Email = "test@domain.com"
        };

        var result = customerService.AddCustomer(customer);

        // Assert
        Assert.True(result);

    }

    [Fact]
    public void AddCustomer_Should_NotAddCustomer_Then_ReturnFalse()
    {
        // Arrange
        var customerRepo = new CustomerRepository(_context);
        var customerService = new CustomerService(customerRepo);

        // Act
        var customer = new Customer
        {
            FirstName = "",
            LastName = "",
            Phone = "",
            Email = ""
        };

        var result = customerService.AddCustomer(customer);


        // Assert
        Assert.False(result);

    }

    [Fact]
    public void GetCustomerByEmail_Should_GetCustomerByEmail_Then_ReturnCustomer()
    {
        // Arrange
        var customerRepo = new CustomerRepository(_context);
        var customerService = new CustomerService(customerRepo);
        var customer = new Customer
        {
            FirstName = "Test",
            LastName = "Test",
            Phone = "1234567890",
            Email = "test@domain.com"
        };

        customerService.AddCustomer(customer);


        // Act
        var result = customerService.GetCustomerByEmail("test@domain.com");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test", result.FirstName);
        Assert.Equal("Test", result.LastName);
        Assert.Equal("1234567890", result.Phone);

    }

    [Fact]
    public void GetCustomerByEmail_Should_NotGetCustomerByEmail_Then_ReturnNull()
    {
        // Arrange
        var customerRepo = new CustomerRepository(_context);
        var customerService = new CustomerService(customerRepo);

        // Act
        var result = customerService.GetCustomerByEmail("test@domain.com");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAllCustomers_Should_GetAllCustomers_Then_ReturnCustomersAsList()
    {
        // Arrange
        var customerRepo = new CustomerRepository(_context);
        var customerService = new CustomerService(customerRepo);
        var customer = new Customer
        {
            FirstName = "Test",
            LastName = "Test",
            Phone = "1234567890",
            Email = "test@domain.com"
        };
        customerService.AddCustomer(customer);

        // Act
        var result = customerService.GetAllCustomers();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<Customer>>(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void GetAllCustomers_Should_NotGetAllCustomers_If_NotExist_Then_ReturnEmptyList()
    {
        // Arrange
        var customerRepo = new CustomerRepository(_context);
        var customerService = new CustomerService(customerRepo);

        // Act
        var result = customerService.GetAllCustomers();

        // Assert
        Assert.Empty(result);
        Assert.IsType<List<Customer>>(result);
    }

    [Fact]
    public void UpdateCustomer_Should_UpdateCustomer_Then_ReturnTrue()
    {
        // Arrange
        var customerRepo = new CustomerRepository(_context);
        var customerService = new CustomerService(customerRepo);
        var customer = new Customer
        {
            FirstName = "Test",
            LastName = "Test",
            Phone = "1234567890",
            Email = "test@domain.com"
        };
        customerService.AddCustomer(customer);

        // Act
        customer.FirstName = "Updated Test";
        customer.LastName = "Updated Test";
        customer.Phone = "0987654321";
        customer.Email = "updated@domain.com";

        var result = customerService.UpdateCustomer(customer);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void UpdateCustomer_Should_NotUpdateCustomer_If_NotExist_Then_ReturnFalse()
    {
        // Arrange
        var customerRepo = new CustomerRepository(_context);
        var customerService = new CustomerService(customerRepo);

        var nonExistingCustomer = new Customer
        {
            FirstName = "Test",
            LastName = "Test",
            Phone = "1234567890",
            Email = "test@domain.com"
        };

        // Act
        var result = customerService.UpdateCustomer(nonExistingCustomer);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void DeleteCustomer_Should_DeleteCustomerByEmail_Then_ReturnTrue()
    {
        // Arrange
        var customerRepo = new CustomerRepository(_context);
        var customerService = new CustomerService(customerRepo);
        var customer = new Customer
        {
            FirstName = "Test",
            LastName = "Test",
            Phone = "1234567890",
            Email = "test@domain.com"
        };

        customerService.AddCustomer(customer);

        // Act
        var result = customerService.DeleteCustomer(customer.Email);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void DeleteCustomer_Should_NotDeleteCustomerByEmail_If_NotExist_Then_ReturnFalse()
    {
        // Arrange
        var customerRepo = new CustomerRepository(_context);
        var customerService = new CustomerService(customerRepo);

        // Act
        var result = customerService.DeleteCustomer("");

        // Assert
        Assert.False(result);
    }
}
