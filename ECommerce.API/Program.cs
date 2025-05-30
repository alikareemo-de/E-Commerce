using AutoMapper;
using ECommerce.Application.Mappings;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Services;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ECommerceDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<OrderService>();

            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            builder.Services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(typeof(ECommerce.Application.Handlers.PlaceOrderCommandHandler).Assembly);
            });


            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new OrderMappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ECommerceDbContext>();
                dbContext.Database.EnsureCreated();
                DatabaseSeeder.Seed(dbContext);
            }

            app.Run();
        }
    }
}
