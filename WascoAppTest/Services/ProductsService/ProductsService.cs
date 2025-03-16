using Microsoft.EntityFrameworkCore;
using WascoAppTest.Data;
using WascoAppTest.Models;
using WascoAppTest.ViewModels.Categories;
using WascoAppTest.ViewModels.Products;

namespace WascoAppTest.Services.ProductsService
{
    public class ProductsService : IProductsService
    {
        private AppDbContext _context;

        public ProductsService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Products> GetAll()
        {
            var products = _context.Products
                            .Include(x => x.Category)
                            .ToList();
            return products;
        }

        public async Task Create(ProductsFormViewModel model)
        {


            Products products = new()
            {
                ProductName = model.ProductName,
                CategoryId = model.CategoryId,
                Price = model.Price
            };

            await _context.Products.AddAsync(products);
            await _context.SaveChangesAsync();

        }

        public Products? GetById(int id)
        {
            var products = _context.Products
                .Include(x => x.Category)
                .AsNoTracking()
                .SingleOrDefault(x => x.ProductId == id);
            return products;
        }

        public Task<Products?> Update(ProductsFormViewModel model)
        {
            var product = _context?.Products.SingleOrDefault(x => x.ProductId == model.ProductId);

            if (product == null) { return Task.FromResult<Products?>(null); }

            product.ProductName = model.ProductName;
            product.CategoryId = model.CategoryId;
            product.Price = model.Price;

            var effectedRows = _context?.SaveChanges();

            if (effectedRows > 0)
            {
                return Task.FromResult<Products?>(product);
            }
            else
            {
                return Task.FromResult<Products?>(null);
            }
        }

        public bool Delete(int id)
        {
            var isDeleted = false;
            var product = _context.Products.Find(id);
            if (product is null) { return isDeleted; }

            _context.Products.Remove(product);
            var effectedRows = _context.SaveChanges();

            if (effectedRows > 0)
            {
                isDeleted = true;
            }
            return isDeleted;
        }



    }
}
