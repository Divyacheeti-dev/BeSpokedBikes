using BeSpokedSalesApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using BeSpokedSalesApp.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeSpokedSalesApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ApiClient<Customer> _apiClient;
        private readonly string _apiEndpoint;
        public CustomerController(ILogger<CustomerController> logger, ApiClient<Customer> apiClient)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<IActionResult> Customers()
        {
            var Customers = await _apiClient.GetAsync("Customers");
            var viewModel = new CustomerListModel { Customers = Customers };
            return View(viewModel);
        }

        public async Task<IActionResult> CreateCustomer()
        {
            CustomerCreateModel customerCreateModel = new CustomerCreateModel();
            return View(customerCreateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCustomer(Customer Customer)
        {
            if (ModelState.IsValid)
            {
                await _apiClient.PostAsync("Customers", Customer);
                return RedirectToAction("Customers");

            }
            else
            {
                CustomerCreateModel customerCreateModel = new CustomerCreateModel()
                {
                    Customer = Customer
                };
                return View(customerCreateModel);
            }
        }
    }

    
}
