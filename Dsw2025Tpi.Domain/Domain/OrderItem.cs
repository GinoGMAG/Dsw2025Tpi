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
        public int quantity { get; set; }
        public decimal unitPrice { get; set; }
        public decimal subtotal
        {
            get { return quantity * unitPrice; }
        }

        public OrderItem() 
        {

        }
        public OrderItem(Guid productId, Guid orderId, int quantity, decimal currentunitPrice)
        {
            this.ProductID = productId;
            this.OrderID = orderId;
            this.quantity = quantity;
            this.unitPrice = currentunitPrice;
        }

    }
}
