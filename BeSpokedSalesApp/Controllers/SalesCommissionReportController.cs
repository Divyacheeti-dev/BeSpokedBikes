using BeSpokedSalesApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using BeSpokedSalesApp.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeSpokedSalesApp.Controllers
{
    public class SalesCommissionReportController : Controller
    {
        private readonly ILogger<SalesCommissionReportController> _logger;
        private readonly string _apiEndpoint;
        private readonly ApiClient<SalesCommissionReport> _apiClient;
        public SalesCommissionReportController(ILogger<SalesCommissionReportController> logger, ApiClient<SalesCommissionReport> apiClient)
        {
            _apiClient = apiClient;
            _logger = logger;
        }


        public async Task<IActionResult> Report(int quarter = 1,int year =2024)
        {
            var SalesCommissionReport = await _apiClient.GetSalesCommissionReportAsync("SalesCommissionReport", quarter,year);

            var viewModel = new SaleCommisionReportListModel { SalesCommissionReports = SalesCommissionReport.ToList() };
            return View(viewModel);
        }
    }

    
}
