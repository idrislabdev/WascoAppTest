using WascoAppTest.Models;
using WascoAppTest.ViewModels.Categories;
using WascoAppTest.ViewModels.Products;

namespace WascoAppTest.Services.ProductsService
{
    public interface IProductsService
    {
        IEnumerable<Products> GetAll();
        Task Create(ProductsFormViewModel model);
        Task<Products?> Update(ProductsFormViewModel model);
        Products? GetById(int id);

        bool Delete(int id);

    }
}
