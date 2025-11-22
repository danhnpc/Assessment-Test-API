using Assessment_Test_BLL.IService;
using Assessment_Test_DAL.Model.PostModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assessment_Test_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _svc;
    public OrderController(IOrderService svc) => _svc = svc;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        var o = await _svc.GetOrderByIdAsync(id);
        if (o == null) return NotFound();
        return Ok(o);
    }

    [HttpGet("customer/{customerId}")]
    public async Task<IActionResult> GetByCustomer(int customerId)
        => Ok(await _svc.GetOrdersByCustomerAsync(customerId));

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderPostModel model)
    {
        var o = await _svc.CreateOrderAsync(model);
        return CreatedAtAction(nameof(GetOrder), new { id = o.Id }, o);
    }
}
