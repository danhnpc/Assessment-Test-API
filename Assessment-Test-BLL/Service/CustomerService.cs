
using Assessment_Test_BLL.IService;
using Assessment_Test_DAL.Model.Entities;
using Assessment_Test_DAL.Model.PostModel;
using Assessment_Test_DAL.Utils;
using Microsoft.EntityFrameworkCore;

namespace Assessment_Test_BLL.Service;

internal class CustomerService : ICustomerService
{
    private readonly ApplicationDbContext _db;
    public CustomerService(ApplicationDbContext db) => _db = db;

    public async Task<Customer> CreateCustomerAsync(CreateCustomerPostModel model)
    {
        var customer = new Customer
        {
            FullName = model.FullName,
            Phone = model.Phone,
            UserId = model.UserId,
            CreatedAt = DateTime.UtcNow
        };

        _db.Customers.Add(customer);
        await _db.SaveChangesAsync();
        return customer;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        => await _db.Customers.ToListAsync();

    public async Task<Customer?> GetCustomerByIdAsync(int id)
        => await _db.Customers.FirstOrDefaultAsync(c => c.Id == id);
}
