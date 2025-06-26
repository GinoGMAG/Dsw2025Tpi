using Dsw2025Tpi.Domain.Entities;


namespace Dsw2025Tpi.Domain.Domain
{
    public class Product : EntityBase
    {   
        public string Sku { get; set; }
        public string InternalCode { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public decimal CurrentUnitPrice { get; set; }
        public int StockQuantity { get; set; }

        public Boolean IsActive { get; private set; } = true;


        public Product(string sku, string internalCode, string description, string name, decimal currentUnitPrice, int stockQuantity)
        {
            Sku = sku;
            this.InternalCode = internalCode;
            Description = description;
            Name = name;
            CurrentUnitPrice = currentUnitPrice;
            StockQuantity = stockQuantity;
        }

        public void SetIsActive() => IsActive = !IsActive;
    
    }
}
