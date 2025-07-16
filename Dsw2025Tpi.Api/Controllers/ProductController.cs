using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Application.Interfaces;
using Dsw2025Tpi.Domain.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace Dsw2025Tpi.Api.Controllers;

[Route("api/products")] // not use a [Controler]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductsManagementsService _service;

    public ProductController(IProductsManagementsService service)
    {
        _service = service;
    }

    //Crete a new product
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductModel.ProductRequestWithDescription request)
    {
        try
        {
            var product = await _service.AddProduct(request);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
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
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductModel.ProductRequestWithDescription request)
    {
        try
        {
            var product = await _service.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            var updatedProduct = await _service.ModifyProduct(product, request);
            return NoContent();
        }
        catch (Exception)
        {
            return Problem("An error occurred while updating the product");
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchProductIsActive(Guid id)
    {
        try
        {
            var product = await _service.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            await _service.PatchProductIsActive(product);
            return NoContent();
        }
        catch (Exception)
        {
            return Problem("An error occurred while patching the product");
        }
    }
}