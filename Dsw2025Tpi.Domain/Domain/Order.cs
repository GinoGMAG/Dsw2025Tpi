using Dsw2025Tpi.Domain.Entities;

namespace Dsw2025Tpi.Domain.Domain
{
    public class Order : EntityBase
    {
        public Guid CustomerId { get; init; }
        public DateTime Date { get; init; } = DateTime.Now;
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }
        public string? Notes { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public OrderStatus OrderStatus { get; set; } = OrderStatus.PENDING;

        // Navigation properties
        public Customer Customer { get; set; }

        public Order(Guid customerId, string shippingAddress, string billingAddress)
        {
            this.CustomerId = customerId;
            this.ShippingAddress = shippingAddress;
            this.BillingAddress = billingAddress;
        }

    }
}
