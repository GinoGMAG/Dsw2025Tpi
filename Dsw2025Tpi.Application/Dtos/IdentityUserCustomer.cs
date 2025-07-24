using Dsw2025Tpi.Domain.Domain;
using Microsoft.AspNetCore.Identity;

namespace Dsw2025Tpi.Application.Dtos;

public class IdentityUserCustomer : IdentityUser
{
    public Guid CustomerId { get; set; }

    }
