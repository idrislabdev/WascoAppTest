using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WascoAppTest.Models;
using WascoAppTest.Services.CategoriesService;
using WascoAppTest.ViewModels.Categories;

namespace WascoAppTest.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ILogger<CategoriesController> logger, ICategoriesService categoriesService)
        {
            _logger = logger;
            _categoriesService = categoriesService;
        }

        [Authorize]
        public IActionResult Index(string searchString)
        {
            var categories = _categoriesService.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(n => n.CategoryName.ToLower().Contains(searchString.ToLower())).ToList();
            }

            return View(categories);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Form()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddOrUpdate(CategoryFormViewModel model)
        {
            if (model.CategoryId == 0)
            {
                await _categoriesService.Create(model);
            }
            else
            {
                await _categoriesService.Update(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Categories/Form/{CategoryId}")]
        [Authorize]
        public IActionResult Form(int CategoryId)
        {
            var category = _categoriesService.GetById(CategoryId);
            if (category == null)
            {
                return NotFound();
            }

            CategoryFormViewModel viewModel = new()
            {
                CategoryName = category.CategoryName,
                CategoryId = category.CategoryId
            };
            return View(viewModel);
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var isDeleted = _categoriesService.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}
