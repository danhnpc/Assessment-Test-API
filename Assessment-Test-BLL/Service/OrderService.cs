using Assessment_Test_BLL.IService;
using Assessment_Test_DAL.Model.Entities;
using Assessment_Test_DAL.Model.PostModel;
using Assessment_Test_DAL.Utils;
using Microsoft.EntityFrameworkCore;

namespace Assessment_Test_BLL.Service;

internal class OrderService : IOrderService
{
    private readonly ApplicationDbContext _db;
    public OrderService(ApplicationDbContext db) => _db = db;

    public async Task<Order?> GetOrderByIdAsync(int id)
        => await _db.Orders.Include(o => o.Items).ThenInclude(i => i.Product).FirstOrDefaultAsync(o => o.Id == id);

    public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId)
        => await _db.Orders.Where(o => o.CustomerId == customerId).Include(o => o.Items).ToListAsync();

    public async Task<Order> CreateOrderAsync(CreateOrderPostModel model)
    {
        if (model.Items == null || !model.Items.Any())
            throw new ArgumentException("Order must contain at least one item", nameof(model));

        // Transaction to ensure stock & order atomicity
        await using var tx = await _db.Database.BeginTransactionAsync();

        var order = new Order
        {
            CustomerId = model.CustomerId,
            CreatedAt = DateTime.UtcNow,
            TotalAmount = 0m
        };

        _db.Orders.Add(order);
        await _db.SaveChangesAsync(); // get order.Id

        decimal total = 0m;
        foreach (var it in model.Items)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == it.ProductId);
            if (product == null)
                throw new InvalidOperationException($"Product {it.ProductId} not found");

            if (product.Stock < it.Quantity)
                throw new InvalidOperationException($"Insufficient stock for product {product.Id}");

            // reduce stock and record unit price at time of order
            product.Stock -= it.Quantity;
            product.UpdatedAt = DateTime.UtcNow;
            _db.Products.Update(product);

            var orderItem = new OrderItem
            {
                OrderId = order.Id,
                ProductId = product.Id,
                Quantity = it.Quantity,
                UnitPrice = product.Price
            };

            _db.OrderItems.Add(orderItem);
            total += orderItem.UnitPrice * orderItem.Quantity;
        }

        order.TotalAmount = total;
        _db.Orders.Update(order);

        await _db.SaveChangesAsync();
        await tx.CommitAsync();
        return order;
    }
}
