using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Dtos;

public record ProductModel
{
    public record ProductRequest(Guid id,int quantity,string name, string description, decimal currentunitPrice);
    public record ProductRequestWithDescription(string Sku,string InternalCode, string Name, string Description, decimal Price,decimal Stock);

    public record ProductResponse(Guid Id);
    public record ProductResponseWithDescription(Guid Id, string Sku, string InternalCode, string Name, string Description, decimal Price, decimal Stock,bool IsActive);
}
