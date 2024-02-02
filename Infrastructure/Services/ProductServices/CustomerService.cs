using Infrastructure.Entities.ProductCatalog;
using Infrastructure.Repositories.ProductRepositories;
using System.Diagnostics;

namespace Infrastructure.Services.ProductServices;

public class CustomerService(CategoryRepository categoryRepository, ProductRepository productRepository, CustomerRepository customerRepository, ManufacturerRepository manufacturerRepository, PurchaseRepository purchaseRepository)
{
    private readonly CategoryRepository _categoryRepository = categoryRepository;
    private readonly ProductRepository _productRepository = productRepository;
    private readonly CustomerRepository _customerRepository = customerRepository;
    private readonly ManufacturerRepository _manufacturerRepository = manufacturerRepository;
    private readonly PurchaseRepository _purchaseRepository = purchaseRepository;


    public bool AddCustomer(Customer customer)
    {
        try
        {
            var customerEntity = _customerRepository.Create(customer);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return false;
    }

    public Customer GetCustomerByEmail(string email)
    {
        try
        {
            var customer = _customerRepository.GetOne(x => x.Email == email);
            return customer;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
    }

    public IEnumerable<Customer> GetAllCustomers()
    {
        try
        {
            var customers = _customerRepository.GetAll();
            return customers;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
    }

    public bool UpdateCustomer(Customer customer)
    {
        try
        {
            var existingCustomer = _customerRepository.GetOne(x => x.CustomerId == customer.CustomerId);
            if (existingCustomer != null)
            {
                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
                existingCustomer.Email = customer.Email;
                existingCustomer.Phone = customer.Phone;

                var customerEntity = _customerRepository.Update(existingCustomer);
                if (customerEntity != null)
                {
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return false;
    }

    public bool DeleteCustomer(string email)
    {
        try
        {
            var existingCustomer = GetCustomerByEmail(email);
            if (existingCustomer != null)
            {
                _customerRepository.Delete(customer => customer.Email == email);
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return false;
    }
}
