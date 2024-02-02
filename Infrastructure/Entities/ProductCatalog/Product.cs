using System;
using System.Collections.Generic;

namespace Infrastructure.Entities.ProductCatalog;

public partial class Product
{
    public string ArticleNumber { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int CategoryId { get; set; }

    public int ManufacturerId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}
