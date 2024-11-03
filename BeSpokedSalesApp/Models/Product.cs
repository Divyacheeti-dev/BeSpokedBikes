using System;
using System.Collections.Generic;

namespace BeSpokedSalesApp.Models;

public class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; }

    public string Manufacturer { get; set; }

    public string Style { get; set; }

    public decimal PurchasePrice { get; set; }

    public decimal SalePrice { get; set; }

    public int QtyOnHand { get; set; }

    public decimal CommissionPercentage { get; set; }

    public List<Discount> Discounts { get; set; } = new List<Discount>();

    public  List<Sale> Sales { get; set; } = new List<Sale>();
}

public class ProductListModel
{
    public IEnumerable<Product> Products { get; set; }
}

public class ProductEditModel
{
    public Product Product { get; set; }
}