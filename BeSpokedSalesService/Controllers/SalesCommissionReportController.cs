using BeSpokedSalesModel.DBModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeSpokedSalesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesCommissionReportController : ControllerBase
    {
        private readonly AppDBContext _context;

        public SalesCommissionReportController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSalesCommissionReport(int quarter, int year, int? salespersonId = null)
        {
            var startDate = GetQuarterStartDate(quarter, year);
            var endDate = GetQuarterEndDate(quarter, year);

            var query = _context.Sales.Include(s => s.Product).Include(s => s.Salesperson)
                .Where(s => s.SalesDate >= DateOnly.FromDateTime(startDate) && s.SalesDate <= DateOnly.FromDateTime(endDate));

            if (salespersonId.HasValue)
            {
                query = query.Where(s => s.SalespersonId == salespersonId);
            }

            var salesData = await query.ToListAsync();

            var reportData = salesData.GroupBy(s => s.SalespersonId)
                .Select(g => new SalesCommissionReport
                {
                    SalespersonName = g.First().Salesperson.FirstName + " " + g.First().Salesperson.LastName,
                    TotalSales = g.Sum(s => s.Product.SalePrice * (1 - (s.Product.CommissionPercentage/100))),
                    CommissionAmount = g.Sum(s => s.Product.SalePrice * s.Product.CommissionPercentage/100)
                }).ToList();

            return Ok(reportData);
        }

        private DateTime GetQuarterStartDate(int quarter, int year)
        {
            switch (quarter)
            {
                case 1:
                    return new DateTime(year, 1, 1);
                case 2:
                    return new DateTime(year, 4, 1);
                case 3:
                    return new DateTime(year, 7, 1);
                case 4:
                    return new DateTime(year, 10, 1);
                default:
                    throw new ArgumentOutOfRangeException(nameof(quarter),"Quarter must be between 1 and 4.");
            }
        }

        private DateTime GetQuarterEndDate(int quarter, int year)
        {
            switch (quarter)
            {
                case 1:
                    return new DateTime(year, 3, 31);
                case 2:
                    return new DateTime(year, 6, 30);
                case 3:
                    return new DateTime(year, 9, 30);
                case 4:
                    return new DateTime(year, 12, 31);
                default:
                    throw new ArgumentOutOfRangeException(nameof(quarter),"Quarter must be between 1 and 4.");
            }
        }

    }

    public class SalesCommissionReport
    {
        public string SalespersonName { get; set; }
        public decimal TotalSales { get; set; }
        public decimal CommissionAmount { get; set; }
    }
}
