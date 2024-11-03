using BeSpokedSalesApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using BeSpokedSalesApp.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeSpokedSalesApp.Controllers
{
    public class SalesPersonController : Controller
    {
        private readonly ILogger<SalesPersonController> _logger;
        private readonly ApiClient<Salesperson> _apiClient;
        private readonly string _apiEndpoint;
        public SalesPersonController(ILogger<SalesPersonController> logger, ApiClient<Salesperson> apiClient)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<IActionResult> Salespersons()
        {
            var salespersons = await _apiClient.GetAsync("Salespeople");
            var viewModel = new SalespersonListModel { Salespersons = salespersons };
            return View(viewModel);
        }

        public async Task<IActionResult> UpdateSalesPerson(int id)
        {
            var salesperson = await _apiClient.GetByIdAsync("Salespeople", id);
            var salespersons = await _apiClient.GetAsync("Salespeople");
            SalespersonEditModel viewModel = new SalespersonEditModel()
            {
                Salesperson = salesperson,
                Managers = salespersons.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSalesPerson(int id, Salesperson salesperson)// [Bind("SalespersonId,FirstName,LastName,Address,Phone,StartDate,TerminationDate,CommissionPercentage,ManagerId")] Salesperson salesperson)
        {
            if (id != salesperson.SalespersonId)
            {
                return NotFound();
            }
            var salespersons = await _apiClient.GetAsync("Salespeople");
            SalespersonEditModel viewModel = new SalespersonEditModel()
            {
                Salesperson = salesperson,
                Managers = salespersons.ToList()
            };

            if (ModelState.IsValid)
            {
                try
                {
                    await _apiClient.PutAsync("Salespeople", id, salesperson);
                }
                catch (Exception)
                {
                   throw;
                }
                return RedirectToAction("Salespersons");
            }
            return View(viewModel);
        }
    }

    
}
