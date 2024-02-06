using Infrastructure.Entities.ProductCatalog;
using Infrastructure.Repositories.ProductRepositories;
using System.Diagnostics;

namespace Infrastructure.Services.ProductServices;

public class ManufacturerService(ManufacturerRepository manufacturerRepository)
{
    private readonly ManufacturerRepository _manufacturerRepository = manufacturerRepository;

    public Manufacturer GetManufacturerByName(string manufacturerName)
    {
        try
        {
            var manufacturer = _manufacturerRepository.GetOne(x => x.ManufacturerName == manufacturerName);
            if (manufacturer != null)
            {
                return manufacturer;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
    }

    public bool DeleteManufacturer(string manufacturerName)
    {
        try
        {
            var existingmanufacturer = GetManufacturerByName(manufacturerName);
            if (existingmanufacturer != null)
            {
                _manufacturerRepository.Delete(category => category.ManufacturerName == manufacturerName);
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
