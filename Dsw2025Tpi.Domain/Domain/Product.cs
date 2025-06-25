using Dsw2025Tpi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Domain
{
    public class Product : EntityBase
    {   
        public string Sku { get; set; }
        public string internalCode { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public decimal CurrentUnitPrice { get; set; }
        public int StockQuantity { get; set; }

        public Boolean isActive
        {
            get { return StockQuantity > 0; }
        }

        public Product(string sku, string internalCode, string description, string name, decimal currentUnitPrice, int stockQuantity)
        {
            Sku = sku;
            this.internalCode = internalCode;
            Description = description;
            Name = name;
            CurrentUnitPrice = currentUnitPrice;
            StockQuantity = stockQuantity;
        }
    }
}
