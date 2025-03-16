using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WascoAppTest.Data;
using WascoAppTest.Models;
using WascoAppTest.ViewModels.Categories;

namespace WascoAppTest.Services.CategoriesService
{
    public class CategoriesService : ICategoriesService
    {
        private AppDbContext _context;

        public CategoriesService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categories> GetAll()
        {
            var categories = _context.Categories.ToList();
            return categories;
        }

        public async Task Create(CategoryFormViewModel model)
        {


            Categories categories = new()
            {
                CategoryName = model.CategoryName,
            };

            await _context.Categories.AddAsync(categories);
            await _context.SaveChangesAsync();

        }

        public Categories? GetById(int id)
        {
            var category = _context.Categories.SingleOrDefault(x => x.CategoryId == id);
            return category;
        }

        public Task<Categories?> Update(CategoryFormViewModel model)
        {
            var category = _context?.Categories.SingleOrDefault(x => x.CategoryId == model.CategoryId);

            if (category == null) { return Task.FromResult<Categories?>(null); }

            category.CategoryName = model.CategoryName;
            var effectedRows = _context?.SaveChanges();

            if (effectedRows > 0)
            {
                return Task.FromResult<Categories?>(category);
            }
            else
            {
                return Task.FromResult<Categories?>(null);
            }
        }

        public bool Delete(int id)
        {
            var isDeleted = false;
            var category = _context.Categories.Find(id);
            if (category is null) { return isDeleted; }

            _context.Categories.Remove(category);
            var effectedRows = _context.SaveChanges();

            if (effectedRows > 0)
            {
                isDeleted = true;
            }
            return isDeleted;
        }

        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _context.Categories.Select(c => new SelectListItem { Value = c.CategoryId.ToString(), Text = c.CategoryName})
               .OrderBy(c => c.Text)
               .AsNoTracking()
               .ToList();
        }

    }
}
