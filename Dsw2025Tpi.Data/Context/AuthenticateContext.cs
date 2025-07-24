using Dsw2025Tpi.Application.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dsw2025Tpi.Data.Context;

public class AuthenticateContext : IdentityDbContext<IdentityUserCustomer, IdentityRole, string>
{
    public AuthenticateContext(DbContextOptions<AuthenticateContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityUserCustomer>(b => { b.ToTable("Usuarios"); });
        builder.Entity<IdentityRole>(b => { b.ToTable("Roles"); });
        builder.Entity<IdentityUserRole<string>>(b => { b.ToTable("UsuarioRoles"); });
        builder.Entity<IdentityUserClaim<string>>(b => { b.ToTable("UsuarioClaims"); });
        builder.Entity<IdentityUserLogin<string>>(b => { b.ToTable("UsuarioLogins"); });
        builder.Entity<IdentityRoleClaim<string>>(b => { b.ToTable("RoleClaims"); });
        builder.Entity<IdentityUserToken<string>>(b => { b.ToTable("UsuarioTokens"); });
    }
}
