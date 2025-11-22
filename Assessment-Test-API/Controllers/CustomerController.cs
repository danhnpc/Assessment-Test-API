using Assessment_Test_BLL.IService;
using Assessment_Test_DAL.Model.PostModel;
using Microsoft.AspNetCore.Mvc;

namespace Assessment_Test_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _svc;
    public CustomerController(ICustomerService svc) => _svc = svc;

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerPostModel model)
    {
        var c = await _svc.CreateCustomerAsync(model);
        return CreatedAtAction(nameof(GetCustomer), new { id = c.Id }, c);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomer(int id)
    {
        var c = await _svc.GetCustomerByIdAsync(id);
        if (c == null) return NotFound();
        return Ok(c);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllCustomersAsync());
}
