using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Dtos;

public record ProductModel
{
    public record Request(string Sku,string Name, decimal Price);
    public record RequestWithDescription(string Sku,string InternalCode, string Name, string Description, decimal Price,decimal Stock);

    public record Response(Guid Id);
    public record class ResponseWithDescription(Guid Id, string Sku, string InternalCode, string Name, string Description, decimal Price, decimal Stock,bool IsActive);
}
