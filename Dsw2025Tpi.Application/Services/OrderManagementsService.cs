using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Application.Interfaces;
using Dsw2025Tpi.Domain.Domain;
using Dsw2025Tpi.Domain.Interfaces;
using System.Linq;
using System.Linq.Expressions;

namespace Dsw2025Tpi.Application.Services;

public class OrderManagementsService : IOrderManagementsService
{
    private IRepository _repository;

    public OrderManagementsService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<OrderModel.OrderResponse> AddOrder(OrderModel.OrderRequest request)
    {

        var exist = await _repository.First<Customer>(c => c.Id == request.customerId);
        if (exist == null) throw new NotExistException($"No existe el Customer con id {request.customerId}");
        if (IsValid(request)) throw new ArgumentException("Valores de Dirrecion no válidos");
        if (request.Products.Count == 0) throw new OrderEmptyException($"No tiene ningun producto cargado al carrito");

        var order = await CreateOrderAsync(request);
        await _repository.Add(order);

        var orderItemResponses = order.OrderItems.Select(oi => new OrderItemsModel.OrderItemResponse(
            oi.ProductID,
            oi.Product.Name,
            oi.Quantity,
            oi.UnitPrice
        )).ToList();

        return new OrderModel.OrderResponse(
            order.CustomerId,
            order.Id,
            order.Date,
            order.ShippingAddress,
            order.BillingAddress,
            order.Notes,
            order.TotalAmount,
            orderItemResponses,
            order.OrderStatus
        );
    }

    private bool IsValid(OrderModel.OrderRequest request)
    {
        return string.IsNullOrEmpty(request.shippingAddress) || string.IsNullOrEmpty(request.billingAddress);
    }

    private async Task<Order> CreateOrderAsync(OrderModel.OrderRequest request)
    {
        Order order = new Order(request.customerId, request.shippingAddress, request.billingAddress);
        foreach (var product in request.Products)
        {
            var productespecific = await _repository.First<Product>(p => p.Id == product.id);
            if (productespecific == null) throw new NotExistException($"No existe el Producto con nombre {product.name}");
            else if (productespecific.StockQuantity < product.quantity) throw new StockInsufficientException($"Stock insuficiente del Producto: {productespecific.Name}");
            OrderItem orderItem = new OrderItem(productespecific.Id, order.Id, product.quantity, productespecific.CurrentUnitPrice);
            order.OrderItems.Add(orderItem);
            order.TotalAmount += productespecific.CurrentUnitPrice * product.quantity;
            productespecific.StockQuantity -= product.quantity;
            await _repository.Update(productespecific);
        }
        return order;
    }

    public async Task<Order?> GetOrderById(Guid id)
    {
        return await _repository.GetById<Order>(id);
    }

    public async Task<OrderModel.OrderResponse> UpdateOrderStatus(Guid id, OrderModel.OrderStatusUpdateRequest request)
    {
        var order = await _repository.GetById<Order>(id);
        if (order == null) throw new NotExistException($"No existe la orden con id: {id}");
        else if (!Enum.IsDefined(typeof(OrderStatus),request.OrderStatus)) throw new NotEstateExistException($"El estado de la orden {request.OrderStatus} no es válido");
        order.OrderStatus = request.OrderStatus;
        await _repository.Update(order);
        var orderItemResponses = order.OrderItems.Select(oi => new OrderItemsModel.OrderItemResponse(
            oi.ProductID,
            oi.Product.Name,
            oi.Quantity,
            oi.UnitPrice
        )).ToList();

        return new OrderModel.OrderResponse(
            order.CustomerId,
            order.Id,
            order.Date,
            order.ShippingAddress,
            order.BillingAddress,
            order.Notes,
            order.TotalAmount,
            orderItemResponses,
            order.OrderStatus
        );
    }

    public async Task<List<OrderModel.OrderResponse>> GetAllOrdersFilter(OrderModel.OrderFilterRequest request)
    {
        Expression<Func<Order, bool>> predicate = o => (!request.OrderStatus.HasValue || o.OrderStatus == request.OrderStatus.Value) &&
        (!request.CustomerId.HasValue || o.CustomerId == request.CustomerId.Value);

        var orders = await _repository.GetFiltered<Order>(predicate, "orderItems");

        if (orders == null || !orders.Any())
        {
            return new List<OrderModel.OrderResponse>();
        }

        return orders.Select(order => new OrderModel.OrderResponse(
            order.CustomerId,
            order.Id,
            order.Date,
            order.ShippingAddress,
            order.BillingAddress,
            order.Notes,
            order.TotalAmount,
            order.OrderItems.Select(oi => new OrderItemsModel.OrderItemResponse(
                oi.ProductID,
                oi.Product?.Name ?? string.Empty,
                oi.Quantity,
                oi.UnitPrice
            )).ToList(),
            order.OrderStatus
        )).ToList();
    }
}
