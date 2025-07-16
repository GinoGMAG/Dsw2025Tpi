using Dsw2025Tpi.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Dtos;

public record OrderModel
{
    public record OrderStatusUpdateRequest(OrderStatus OrderStatus);
    public record OrderRequest(Guid customerId, string shippingAddress, string billingAddress, List<ProductModel.ProductRequest> Products);
    public record OrderFilterRequest(OrderStatus? OrderStatus,Guid? CustomerId, int? pageNumber, int? pagesize);
    public record OrderResponse(Guid customerId,Guid OrderId, DateTime date, string shippingAddress, string billingAddress,string notes,decimal totalmount,List<OrderItem> OrderItems,OrderStatus OrderStatus);
}
