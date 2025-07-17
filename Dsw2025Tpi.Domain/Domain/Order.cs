using Dsw2025Tpi.Domain.Entities;

namespace Dsw2025Tpi.Domain.Domain
{
    public class Order : EntityBase
    {
        public Guid customerId { get; init; }
        public DateTime date { get; init; } = DateTime.Now;
        public string shippingAddress { get; set; }
        public string billingAddress { get; set; }
        public string? notes { get; set; }
        public decimal totalAmount { get; set; }
        public List<OrderItem> orderItems { get; set; } = new List<OrderItem>();
        public OrderStatus OrderStatus { get; set; } = OrderStatus.PENDING;

        // Navigation properties
        public Customer Customer { get; set; }

        public Order(Guid customerId, string shippingAddress, string billingAddress)
        {
            this.customerId = customerId;
            this.shippingAddress = shippingAddress;
            this.billingAddress = billingAddress;
        }

    }
}
