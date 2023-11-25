using Application.Contracts;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Dashboard.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserReposatory _userse;
        private readonly IOrderReposatory _order;
        private readonly IProductReposatory _products;
        private readonly ISubCategoryReposatory _subCategories;

        public HomeController(ILogger<HomeController> logger, IUserReposatory userse ,
            IOrderReposatory orders,
            IProductReposatory products,
            ISubCategoryReposatory subCategories
            
            )
        {
            _logger = logger;
            _userse = userse;
            _order = orders;
            _products = products;
            _subCategories = subCategories;
        }

        public async Task<IActionResult> Index()
        {
            var users =await _userse.GetAllAsync();
            int usersCount = users.Count();

            var orders= await _order.GetAllAsync();
            ViewData["orders"] = orders.Count();
            var products = await _products.GetAllAsync();
            ViewData["products"] = products.Count();
            var subCategories = await _subCategories.GetAllAsync();
            ViewData["subCategories"] = subCategories.Count();
            return View(usersCount);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }

		public async Task<IActionResult> GetData()
		{
			var newSubCategory = await _subCategories.GetAllAsync();
			var sub = await newSubCategory.FirstOrDefaultAsync(sc => sc.Name == "New");

			var newProductsCount = await _products.GetAllAsync();
			var productsNewCount = await newProductsCount.CountAsync(p => p.CategoryId == sub.Id);

			var usedSubCategory = await _subCategories.GetAllAsync();
			var sub1 = await usedSubCategory.FirstOrDefaultAsync(sc => sc.Name == "Used");

			var usedCategory = await _products.GetAllAsync();
			var productsUsedCount = await usedCategory.CountAsync(p => p.CategoryId == sub1.Id);

			// Create a dictionary to store the counts
			var ratio = new Dictionary<string, int>
			{
				{ "New", productsNewCount },
				{ "Used", productsUsedCount }
			};

			// Return JSON data
			return Json(ratio);
		}


	}
}