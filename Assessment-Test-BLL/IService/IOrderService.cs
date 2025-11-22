
using Assessment_Test_DAL.Model.Entities;
using Assessment_Test_DAL.Model.PostModel;

namespace Assessment_Test_BLL.IService;

public interface IOrderService
{
    Task<Order?> GetOrderByIdAsync(int id);
    Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId);
    Task<Order> CreateOrderAsync(CreateOrderPostModel model);
}
