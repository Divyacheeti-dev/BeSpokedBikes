namespace BeSpokedSalesApp.Models;
public partial class Customer
{
    public int CustomerId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Address { get; set; }

    public string Phone { get; set; }

    public DateOnly StartDate { get; set; }

    public  List<Sale> Sales { get; set; } = new List<Sale>();
}

public class CustomerListModel
{
    public IEnumerable<Customer> Customers { get; set; }
}

public class CustomerCreateModel
{
    public Customer Customer { get; set; }
}