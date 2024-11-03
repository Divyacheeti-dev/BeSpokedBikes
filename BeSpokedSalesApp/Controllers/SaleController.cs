using BeSpokedSalesApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using BeSpokedSalesApp.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeSpokedSalesApp.Controllers
{
    public class SaleController : Controller
    {
        private readonly ILogger<SaleController> _logger;
        private readonly ApiClient<Sale> _apiClient;
        private readonly ApiClient<Salesperson> _apiClientSalesperson;
        private readonly ApiClient<Product> _apiClientProduct;
        private readonly ApiClient<Customer> _apiClientCustomer;
        private readonly string _apiEndpoint;
        public SaleController(ILogger<SaleController> logger, ApiClient<Sale> apiClient, ApiClient<Salesperson> apiClientSalesperson,
            ApiClient<Product> apiClientProduct, ApiClient<Customer> apiClientCustomer)
        {
            _apiClient = apiClient;
            _apiClientCustomer = apiClientCustomer;
            _apiClientProduct = apiClientProduct;
            _apiClientSalesperson = apiClientSalesperson;
            _logger = logger;
        }

        //public async Task<IActionResult> Sales()
        //{
        //    var Sales = await _apiClient.GetAsync("Sales");
        //    var viewModel = new SaleListModel { Sales = Sales.ToList() };
        //    return View(viewModel);
        //}

        public async Task<IActionResult> Sales(DateOnly? startDate, DateOnly? endDate)
        {
            var Sales = await _apiClient.GetAsync("Sales");

            if (startDate.HasValue && endDate.HasValue)
            {
                Sales = Sales.Where(s => s.SalesDate >= startDate && s.SalesDate <= endDate).ToList();
            }

            var viewModel = new SaleListModel { Sales = Sales.ToList() };
            return View(viewModel);
        }

        public async Task<IActionResult> CreateSale()
        {
            SaleCreateModel viewModel = new SaleCreateModel();
            var Products = await _apiClientProduct.GetAsync("products");
            var Salespersons = await _apiClientSalesperson.GetAsync("Salespeople");
            var Customers = await _apiClientCustomer.GetAsync("customers");
            viewModel.Salespersons = Salespersons != null ?  Salespersons.ToList() : new List<Salesperson>();
            viewModel.Products = Products != null ? Products.ToList() : new List<Product>();
            viewModel.Customers = Customers != null ? Customers.ToList() : new List<Customer>();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSale(Sale Sale)
        {
            if (ModelState.IsValid)
            {
                await _apiClient.PostAsync("Sales", Sale);
                return RedirectToAction("Sales");

            }
            else
            {
                SaleCreateModel viewModel = new SaleCreateModel()
                {
                    Sale = Sale
                };
                var Products = await _apiClientProduct.GetAsync("products");
                var Salespersons = await _apiClientSalesperson.GetAsync("Salespeople");
                var Customers = await _apiClientCustomer.GetAsync("customers");
                viewModel.Salespersons = Salespersons != null ? Salespersons.ToList() : new List<Salesperson>();
                viewModel.Products = Products != null ? Products.ToList() : new List<Product>();
                viewModel.Customers = Customers != null ? Customers.ToList() : new List<Customer>();
                return View(Sale);
            }
            
        }
    }

    
}
