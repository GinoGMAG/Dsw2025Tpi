using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Dtos;

public record OrderItemsModel
{
    public record OrderItem(Guid ProductId, Guid OrderId, int Quantity, decimal UnitPrice);
    public record OrderItemRequest(Guid ProductId, int Quantity);
    public record OrderItemResponse(Guid ProductId, string Name, int Quantity, decimal UnitPrice);
}
