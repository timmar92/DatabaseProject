using Infrastructure.Entities.ProductCatalog;
using Infrastructure.Repositories.ProductRepositories;
using System.Diagnostics;

namespace Infrastructure.Services.ProductServices;

public class ProductService(CategoryRepository categoryRepository, ProductRepository productRepository, CustomerRepository customerRepository, ManufacturerRepository manufacturerRepository, PurchaseRepository purchaseRepository)
{
    private readonly CategoryRepository _categoryRepository = categoryRepository;
    private readonly ProductRepository _productRepository = productRepository;
    private readonly CustomerRepository _customerRepository = customerRepository;
    private readonly ManufacturerRepository _manufacturerRepository = manufacturerRepository;
    private readonly PurchaseRepository _purchaseRepository = purchaseRepository;


    public bool AddProduct(Product product, Manufacturer manufacturer, Category category)
    {

        try
        {
            var categoryEntity = _categoryRepository.GetOne(x => x.CategoryName == category.CategoryName);
            if (categoryEntity == null)
            {
                categoryEntity = _categoryRepository.Create(category);
            }

            var manufacturerEntity = _manufacturerRepository.GetOne(x => x.ManufacturerName == manufacturer.ManufacturerName);
            if (manufacturerEntity == null)
            {
                manufacturerEntity = _manufacturerRepository.Create(manufacturer);
            }

            product.CategoryId = categoryEntity.CategoryId;
            product.ManufacturerId = manufacturerEntity.ManufacturerId;

            var productEntity = _productRepository.Create(product);

            return true;

        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return false;


    }

    public Product GetProductByArticleNumber(string articleNumber)
    {
        try
        {
            var product = _productRepository.GetOne(x => x.ArticleNumber == articleNumber);
            return product;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;

    }

    public IEnumerable<Product> GetAllProducts()
    {

        try
        {
            var products = _productRepository.GetAll();
            return products;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
    }

    public IEnumerable<Product> GetProductsByCategory(string category)
    {
        try
        {
            var categoryEntity = _categoryRepository.GetOne(x => x.CategoryName == category);
            if (categoryEntity != null)
            {
                return categoryEntity.Products;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
    }

    public IEnumerable<Product> GetProductsByManufacturer(string manufacturer)
    {
        try
        {
            var manufacturerEntity = _manufacturerRepository.GetOne(x => x.ManufacturerName == manufacturer);
            if (manufacturerEntity != null)
            {
                return manufacturerEntity.Products;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;

    }

    public bool UpdateProduct(Product product)
    {
        try
        {
            var existingProduct = _productRepository.GetOne(x => x.ArticleNumber == product.ArticleNumber);
            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;

                if (product.Category != null)
                {
                    existingProduct.Category.CategoryName = product.Category.CategoryName;
                }

                if (product.Manufacturer != null)
                {
                    existingProduct.Manufacturer.ManufacturerName = product.Manufacturer.ManufacturerName;
                }

                var productEntity = _productRepository.Update(existingProduct);
                if (productEntity != null)
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

    public bool DeleteProduct(string articleNumber)
    {
        try
        {
            var existingProduct = GetProductByArticleNumber(articleNumber);
            if (existingProduct != null)
            {
                _productRepository.Delete(product => product.ArticleNumber == articleNumber);
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
