using System;
using System.Collections.Generic;

namespace BeSpokedSalesApp.Models;

public class Sale
{
    public int SaleId { get; set; }

    public int? ProductId { get; set; }

    public int? SalespersonId { get; set; }

    public int? CustomerId { get; set; }

    public DateOnly SalesDate { get; set; }

    public Customer? Customer { get; set; }

    public Product? Product { get; set; }

    public Salesperson? Salesperson { get; set; }
}

public class SaleListModel
{
    public List<Sale> Sales { get; set; }
}

public class SaleCreateModel
{
    public Sale Sale { get; set; }
    public List<Product> Products { get; set; }
    public List<Salesperson> Salespersons { get; set; }
    public List<Customer> Customers { get; set; }
}

public class SalesCommissionReport
{
    public string SalespersonName { get; set; }
    public decimal TotalSales { get; set; }
    public decimal CommissionAmount { get; set; }
}

public class SaleCommisionReportListModel
{
    public List<SalesCommissionReport> SalesCommissionReports { get; set; }
}