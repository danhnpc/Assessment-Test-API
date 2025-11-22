
using Assessment_Test_DAL.Model.Entities;
using Assessment_Test_DAL.Model.PostModel;

namespace Assessment_Test_BLL.IService;

public interface IProductService
{
    Task<Product> GetProductByIdAsync(int id);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);
    Task<Product> CreateProductAsync(CreateProductPostModel model);
    Task<Product> UpdateProductAsync(int id, UpdateProductPostModel model);
    Task<bool> DeleteProductAsync(int id);
    Task<bool> UpdateStockAsync(int productId, int quantityChange);
}
