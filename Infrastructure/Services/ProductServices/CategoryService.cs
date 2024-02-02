using Infrastructure.Entities.ProductCatalog;
using Infrastructure.Repositories.ProductRepositories;
using System.Diagnostics;

namespace Infrastructure.Services.ProductServices;

public class CategoryService(CategoryRepository categoryRepository)
{
    private readonly CategoryRepository _categoryRepository = categoryRepository;


    public Category GetCategoryByName(string categoryName)
    {
        try
        {
            var category = _categoryRepository.GetOne(x => x.CategoryName == categoryName);
            if (category != null)
            {
                return category;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
    }

    public bool DeleteCategory(string categoryName)
    {
        try
        {
            var existingCategory = GetCategoryByName(categoryName);
            if (existingCategory != null)
            {
                _categoryRepository.Delete(category => category.CategoryName == categoryName);
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
