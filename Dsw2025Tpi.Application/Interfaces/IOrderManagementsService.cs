using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Domain.Domain;

namespace Dsw2025Tpi.Application.Interfaces
{
    public interface IOrderManagementsService
    {
        Task<OrderModel.OrderResponse> AddOrder(OrderModel.OrderRequest request);
        Task<Order?> GetOrderById(Guid id);
        Task<OrderModel.OrderResponse> UpdateOrderStatus(Guid id, OrderModel.OrderStatusUpdateRequest request);
        Task<List<Order>> GetAllOrdersFilter(OrderModel.OrderFilterRequest request);
    }
}