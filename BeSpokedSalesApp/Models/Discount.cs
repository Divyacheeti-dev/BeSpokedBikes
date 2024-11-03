namespace BeSpokedSalesApp.Models;

public partial class Discount
{
    public int DiscountId { get; set; }

    public int? ProductId { get; set; }

    public DateOnly BeginDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal DiscountPercentage { get; set; }

    public Product Product { get; set; }
}