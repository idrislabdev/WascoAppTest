using Microsoft.AspNetCore.Mvc.Rendering;
using WascoAppTest.Models;
using WascoAppTest.ViewModels.Categories;

namespace WascoAppTest.Services.CategoriesService
{
    public interface ICategoriesService
    {
        IEnumerable<Categories> GetAll();
        Task Create(CategoryFormViewModel model);
        Task<Categories?> Update(CategoryFormViewModel model);
        Categories? GetById(int id);

        bool Delete(int id);
        IEnumerable<SelectListItem> GetSelectList();


    }
}
