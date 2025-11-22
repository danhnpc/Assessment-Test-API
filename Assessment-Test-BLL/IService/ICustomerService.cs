using Assessment_Test_DAL.Model.Entities;
using Assessment_Test_DAL.Model.PostModel;

namespace Assessment_Test_BLL.IService;

public interface ICustomerService
{
    Task<Customer?> GetCustomerByIdAsync(int id);
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer> CreateCustomerAsync(CreateCustomerPostModel model);
}
