using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Domain.Domain;

namespace Dsw2025Tpi.Application.Interfaces
{
    public interface IProductsManagementsService
    {
        Task<ProductModel.ResponseWithDescription> AddProduct(ProductModel.RequestWithDescription request);
        Task<Product> DeleteProduct(Product product);
        Task<IEnumerable<Product>?> GetAllProducts();
        Task<Product?> GetProductById(Guid id);
        Task<Product?> GetProductBySku(string sku);
        Task<Product> ModifyProduct(Product product, ProductModel.RequestWithDescription request);
        Task PatchProductIsActive(Product product);
        Task<Product> UpdateProduct(Product product);


    }
}