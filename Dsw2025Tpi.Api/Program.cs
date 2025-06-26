using Dsw2025Tpi.Application.Interfaces;
using Dsw2025Tpi.Application.Services;
using Dsw2025Tpi.Data;
using Dsw2025Tpi.Data.Repositories;
using Dsw2025Tpi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace Dsw2025Tpi.Api;

public class Program
{
    public static void Main(string[] args)
    {

        string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DSW2025TPI;Integrated Security=True"; // Change this to your actual connection string
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHealthChecks();
        builder.Services.AddDbContext<Dsw2025TpiContext>(options =>
    options.UseSqlServer(ConnectionString));
        builder.Services.AddTransient<IProductsManagementsService, ProductsManagementsService>();
        builder.Services.AddScoped<IRepository, EfRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        
        app.MapHealthChecks("/healthcheck");

        app.Run();
    }
}
