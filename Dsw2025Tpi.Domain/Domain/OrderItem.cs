using Dsw2025Tpi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Domain
{
    public class OrderItem : EntityBase
    {

        public Guid ProductID { get; init; }
        public Guid OrderID { get; init; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal
        {
            get { return Quantity * UnitPrice; }
        }

        // Navigation properties 
        public Order Order { get; set; }    
        public Product Product { get; set; }

        public OrderItem() 
        {

        }
        public OrderItem(Guid productId, Guid orderId, int quantity, decimal currentunitPrice)
        {
            this.ProductID = productId;
            this.OrderID = orderId;
            this.Quantity = quantity;
            this.UnitPrice = currentunitPrice;
        }

    }
}
