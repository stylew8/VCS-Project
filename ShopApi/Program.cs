
using DataAccess;
using DataAccess.Models;
using DataAccess.Repositories;
using DataAccessNew.Models;
using DataAccessNew.Repositories;
using Microsoft.EntityFrameworkCore;
using ShopApi.Services;
using ShopApi.Services.OrderLines;
using ShopApi.Services.Orders;
using ShopApi.Services.Products;
using ShopApi.Services.Users;

namespace ShopApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("Prod");

            builder.Services.AddDbContext<ShopDbContext>(
                    options => options.UseSqlServer(connectionString)
            );


            // repozitoriju registravimas
            builder.Services.AddScoped<IRepository<User>, UserRepository>();
            builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
            builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
            builder.Services.AddScoped<IRepository<OrderLine>, OrderLineRepository>();


            // Add services to the container.
            builder.Services.AddScoped<CreateUserService>();
            builder.Services.AddScoped<CreateProductService>();
            builder.Services.AddScoped<CreateOrderLineService>();
            builder.Services.AddScoped<CreateOrderService>();
            builder.Services.AddScoped<LoggingService>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
           // {
                app.UseSwagger();
                app.UseSwaggerUI();
           // }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}