using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Application.Interfaces;
using Dsw2025Tpi.Domain.Domain;
using Dsw2025Tpi.Domain.Interfaces;


namespace Dsw2025Tpi.Application.Services;

public class ProductsManagementsService : IProductsManagementsService
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
        return await _repository.GetFiltered<Product>(p => p.IsActive);
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

    public async Task<ProductModel.ProductResponseWithDescription> AddProduct(ProductModel.ProductRequestWithDescription request)
    {
        if (!IsValid(request))
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
        return new ProductModel.ProductResponseWithDescription(product.Id, product.Sku, product.InternalCode, product.Name, product.Description, product.CurrentUnitPrice, product.StockQuantity, product.IsActive);
    }

    public async Task<Product> ModifyProduct(Product product, ProductModel.ProductRequestWithDescription request)
    {
        if (!IsValid(request))
        {
            throw new ArgumentException("Valores para el producto no válidos");
        }
        var exist = await _repository.First<Product>(p => p.Sku == request.Sku && p.Id != product.Id);
        if (exist != null) throw new DuplicatedEntityException($"Ya existe un producto con el Sku {request.Sku}");
        product.Sku = request.Sku;
        product.InternalCode = request.InternalCode;
        product.Name = request.Name;
        product.Description = request.Description;
        product.CurrentUnitPrice = request.Price;
        product.StockQuantity = (int)request.Stock;
        return await UpdateProduct(product);
    }

    public async Task PatchProductIsActive(Product product)
    {
        product.SetIsActive();   
        await UpdateProduct(product);
    }

    private bool IsValid(ProductModel.ProductRequestWithDescription request)
    {
        return string.IsNullOrWhiteSpace(request.Sku) ||
            string.IsNullOrWhiteSpace(request.Name) ||
            request.Price > 0 || request.Stock >= 1;
    }


}
