using System;
using System.Collections.Generic;

namespace Infrastructure.Entities.ProductCatalog;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}
