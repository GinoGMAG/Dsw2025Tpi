using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Domain.Domain;
using Dsw2025Tpi.Domain.Interfaces;


namespace Dsw2025Tpi.Application.Services;

public class ProductsManagementsService
{
    private readonly IRepository _repository;

    public ProductsManagementsService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Product?> GetProductById(Guid id)
    {
        return await _repository.GetById<Product>(id);
    }

    public async Task<IEnumerable<Product>?> GetAllProducts()
    {
        return await _repository.GetAll<Product>();
    }

    public async Task<Product> UpdateProduct(Product product)
    {
        return await _repository.Update(product);
    }

    public async Task<Product> DeleteProduct(Product product)
    {
        return await _repository.Delete(product);
    }

    public async Task<Product?> GetProductBySku(string sku)
    {
        return await _repository.First<Product>(p => p.Sku == sku);
    }

    public async Task<ProductModel.Response> AddProduct(ProductModel.RequestWithDescription request)
    {
        if (string.IsNullOrWhiteSpace(request.Sku) ||
            string.IsNullOrWhiteSpace(request.Name) ||
            request.Price < 0)
        {
            throw new ArgumentException("Valores para el producto no válidos");
        }

        var exist = await _repository.First<Product>(p => p.Sku == request.Sku);
        if (exist != null) throw new DuplicatedEntityException($"Ya existe un producto con el Sku {request.Sku}");

        var product = new Product(
            request.Sku,
            request.InternalCode,
            request.Description,
            request.Name,
            request.Price,
            (int)request.Stock
        );
        await _repository.Add(product);
        return new ProductModel.Response(product.Id);
    }
}
