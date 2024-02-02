using Infrastructure.Entities.ProductCatalog;
using Infrastructure.Repositories.ProductRepositories;
using System.Diagnostics;

namespace Infrastructure.Services.ProductServices;

public class PurchaseService(CategoryRepository categoryRepository, ProductRepository productRepository, CustomerRepository customerRepository, ManufacturerRepository manufacturerRepository, PurchaseRepository purchaseRepository)
{
    private readonly CategoryRepository _categoryRepository = categoryRepository;
    private readonly ProductRepository _productRepository = productRepository;
    private readonly CustomerRepository _customerRepository = customerRepository;
    private readonly ManufacturerRepository _manufacturerRepository = manufacturerRepository;
    private readonly PurchaseRepository _purchaseRepository = purchaseRepository;



    public bool AddPurchase(Purchase purchase, Customer customer, Product product)
    {
        try
        {
            var customerEntity = _customerRepository.GetOne(x => x.CustomerId == customer.CustomerId);
            if (customerEntity == null)
            {
                customerEntity = _customerRepository.Create(customer);
            }

            var productEntity = _productRepository.GetOne(x => x.ArticleNumber == product.ArticleNumber);
            if (productEntity == null)
            {
                productEntity = _productRepository.Create(product);
            }

            purchase.CustomerId = customer.CustomerId;
            purchase.ProductArticleNumber = product.ArticleNumber;

            var purchaseEntity = _purchaseRepository.Create(purchase);

            return true;

        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return false;
    }

    public IEnumerable<Purchase> GetPurchasesbyCustomer(int customerId)
    {
        try
        {
            var purchases = _purchaseRepository.GetAll(x => x.CustomerId == customerId);
            return purchases;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
    }
}
