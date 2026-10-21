using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersApi.Data;
using OrdersApi.Models;

namespace OrdersApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(OrdersDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        var orders = await context.Orders.ToListAsync();

        return Ok(orders);
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order)
    {
        context.Orders.Add(order);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrders), new { id = order.Id }, order);
    }
}