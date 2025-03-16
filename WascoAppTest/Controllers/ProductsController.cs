using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WascoAppTest.Models;
using WascoAppTest.Services.CategoriesService;
using WascoAppTest.Services.ProductsService;
using WascoAppTest.ViewModels.Categories;
using WascoAppTest.ViewModels.Products;

namespace WascoAppTest.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly IProductsService _productService;
        private readonly ICategoriesService _categoriesService;

        public ProductsController(ILogger<CategoriesController> logger, IProductsService productService, ICategoriesService categoriesService)
        {
            _logger = logger;
            _productService = productService;
            _categoriesService = categoriesService;
        }

        [Authorize]
        public IActionResult Index(string searchString)
        {
            var products = _productService.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(n => n.ProductName.ToLower()
                .Contains(searchString.ToLower())).ToList();
            }
            return View(products);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Form()
        {
            ProductsFormViewModel viewModel = new()
            {
                Categories = _categoriesService.GetSelectList(),
            };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddOrUpdate(ProductsFormViewModel model)
        {
            if (model.ProductId == 0)
            {
                await _productService.Create(model);
            }
            else
            {
                await _productService.Update(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Products/Form/{ProductId}")]
        [Authorize]
        public IActionResult Form(int ProductId)
        {
            var product = _productService.GetById(ProductId);
            if (product == null)
            {
                return NotFound();
            }

            ProductsFormViewModel viewModel = new()
            {
                Categories = _categoriesService.GetSelectList(),
                CategoryId = product.CategoryId,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price
            };
            return View(viewModel);
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var isDeleted = _productService.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}
