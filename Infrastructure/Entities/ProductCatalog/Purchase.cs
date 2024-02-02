using System;
using System.Collections.Generic;

namespace Infrastructure.Entities.ProductCatalog;

public partial class Purchase
{
    public int PurchaseId { get; set; }

    public int CustomerId { get; set; }

    public string? ProductArticleNumber { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Product? ProductArticleNumberNavigation { get; set; }
}
