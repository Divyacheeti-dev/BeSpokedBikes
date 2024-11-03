using BeSpokedSalesApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using BeSpokedSalesApp.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BeSpokedSalesApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ApiClient<Product> _apiClient;
        private readonly string _apiEndpoint;
        public ProductController(ILogger<ProductController> logger, ApiClient<Product> apiClient)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<IActionResult> Products()
        {
            var products = await _apiClient.GetAsync("products");
            var viewModel = new ProductListModel { Products = products };
            return View(viewModel);
        }

        public async Task<IActionResult> UpdateProduct(int id)
        {
            var product = await _apiClient.GetByIdAsync("Products", id);
            ProductEditModel viewModel = new ProductEditModel()
            {
                Product = product
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct(int id, [Bind("ProductId,Name,Manufacturer,Style,PurchasePrice,SalePrice,QtyOnHand,CommissionPercentage")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }
            ProductEditModel viewModel = new ProductEditModel()
            {
                Product = product
            };

            if (ModelState.IsValid)
            {
                try
                {
                    await _apiClient.PutAsync("Products", id, product);
                }
                catch (Exception)
                {
                   throw;
                }
                return RedirectToAction("Products");
            }
            return View(viewModel);
        }
    }

    
}
