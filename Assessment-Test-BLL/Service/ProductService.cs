using Assessment_Test_BLL.IService;
using Assessment_Test_DAL.Model.Entities;
using Assessment_Test_DAL.Model.PostModel;
using Assessment_Test_DAL.Utils;
using Microsoft.EntityFrameworkCore;

namespace Assessment_Test_BLL.Service;

internal class ProductService : IProductService
{
    private readonly ApplicationDbContext _db;

    public ProductService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _db.Products.FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _db.Products.Where(p => p.IsActive).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
    {
        return await _db.Products
            .Where(p => p.Category == category && p.IsActive)
            .ToListAsync();
    }

    public async Task<Product> CreateProductAsync(CreateProductPostModel model)
    {
        var product = new Product
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            Stock = model.Stock,
            Sku = model.Sku,
            Category = model.Category,
            ImageUrl = model.ImageUrl
        };

        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateProductAsync(int id, UpdateProductPostModel model)
    {
        var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
            return null;

        if (!string.IsNullOrWhiteSpace(model.Name))
            product.Name = model.Name;
        if (!string.IsNullOrWhiteSpace(model.Description))
            product.Description = model.Description;
        if (model.Price.HasValue)
            product.Price = model.Price.Value;
        if (model.Stock.HasValue)
            product.Stock = model.Stock.Value;
        if (!string.IsNullOrWhiteSpace(model.Sku))
            product.Sku = model.Sku;
        if (!string.IsNullOrWhiteSpace(model.Category))
            product.Category = model.Category;
        if (model.IsActive.HasValue)
            product.IsActive = model.IsActive.Value;
        if (model.ImageUrl != null)
            product.ImageUrl = model.ImageUrl;

        product.UpdatedAt = DateTime.UtcNow;
        _db.Entry(product).Property(x => x.UpdatedAt).IsModified = false;
        await _db.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
            return false;

        _db.Products.Remove(product);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateStockAsync(int productId, int quantityChange)
    {
        var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == productId);
        if (product == null)
            return false;

        product.Stock += quantityChange;
        //product.UpdatedAt = DateTime.SpecifyKind(product.UpdatedAt, DateTimeKind.Utc);
        //_db.Products.Update(product);
        _db.Entry(product).Property(x => x.UpdatedAt).IsModified = false;
        await _db.SaveChangesAsync();
        return true;
    }
}
