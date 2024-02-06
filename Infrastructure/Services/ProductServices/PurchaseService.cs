using Infrastructure.Entities.ProductCatalog;
using Infrastructure.Repositories.ProductRepositories;
using System.Diagnostics;

namespace Infrastructure.Services.ProductServices;

public class PurchaseService(ProductRepository productRepository, CustomerRepository customerRepository, PurchaseRepository purchaseRepository)
{
    private readonly ProductRepository _productRepository = productRepository;
    private readonly CustomerRepository _customerRepository = customerRepository;
    private readonly PurchaseRepository _purchaseRepository = purchaseRepository;



    public bool AddPurchase(Purchase purchase, Customer customer, Product product)
    {
        try
        {
            var customerEntity = _customerRepository.GetOne(x => x.CustomerId == customer.CustomerId);
            if (customerEntity == null)
            {
                return false;
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
