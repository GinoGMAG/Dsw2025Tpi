using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2025Tpi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ProductsManagementsService _service;

    public ProductController(ProductsManagementsService service)
    {
        _service = service;
    }

    //Crete a new product
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductModel.RequestWithDescription request)
    {
        try
        {
            var product = await _service.AddProduct(request);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id}, product);
        }
        catch (ArgumentException ae)
        {
            return BadRequest(ae.Message);
        }
        catch (DuplicatedEntityException de)
        {
            return Conflict(de.Message);
        }
        catch (Exception)
        {
            return Problem("An error occurred while saving the product");
        }
    }
    // Get all products
    [HttpGet()]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            var products = await _service.GetAllProducts();
            if (products == null || !products.Any())
            {
                return NoContent();
            }
            return Ok(products);
        }
        catch (ArgumentException ae)
        {
            return BadRequest(ae.Message);
        }
        catch (DuplicatedEntityException de)
        {
            return Conflict(de.Message);
        }
        catch (Exception)
        {
            return Problem("An error occurred while retrieving the products");
        }
    }


    // Get a product by GUID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        try
        {
            var product = await _service.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        catch (Exception)
        {
            return Problem("An error occurred while retrieving the product");
        }
    }

    // PUT to update a product
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductModel.RequestWithDescription request)
    {
        try
        {
            var product = await _service.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            var updatedProduct = await _service.ModifyProduct(product, request);
            return Ok(updatedProduct);
        }
        catch (Exception)
        {
            return Problem("An error occurred while updating the product");
        }
    }
}