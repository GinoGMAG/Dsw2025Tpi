using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Dsw2025Tpi.Api.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderControler : ControllerBase
{
    private readonly IOrderManagementsService _service;

    public OrderControler(IOrderManagementsService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> addOrder([FromBody] OrderModel.OrderRequest request)
    {
        try
        {
            var order = await _service.AddOrder(request);
            return CreatedAtAction(nameof(GetOrderByID), new { id = order.OrderId }, order);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderByID(Guid id)
    {
        try
        {
            var order = await _service.GetOrderById(id);
            if (order == null)
            {
                return NotFound($"No existe la orden con id: {id}");
            }
            return Ok(order);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders([FromQuery] OrderModel.OrderFilterRequest request)
    {
        try
        {
            var orders = await _service.GetAllOrdersFilter(request);
            return Ok(orders);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] OrderModel.OrderStatusUpdateRequest request)
    {
        try
        {
            var order = await _service.UpdateOrderStatus(id, request);
            return Ok(order);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

