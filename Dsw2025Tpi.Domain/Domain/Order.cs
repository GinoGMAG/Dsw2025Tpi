using Dsw2025Tpi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dsw2025Tpi.Domain.Domain
{
    public class Order : EntityBase
    {
        public DateTime date { get; set; }
        public string shippingAddress { get; set; }
        public string billingAddress { get; set; }
        public string notes { get; set; }
        public decimal totalAmount { get; set; }

        public List<OrderItem> orderItems { get; set; } = new List<OrderItem>();

        public OrderStatus OrderStatus { get; set; }

    }
}
