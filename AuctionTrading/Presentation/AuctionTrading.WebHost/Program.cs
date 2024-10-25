using AuctionTrading.Application.Services;
using AuctionTrading.Application.Services.Abstractions;
using AuctionTrading.Application.Services.Mapping;
using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Repositories.Abstractions;
using AuctionTrading.Infrastructure.EntityFramework;
using AuctionTrading.Infrastructure.Repositories.Implementations.EF;
using AuctionTrading.WebHost.Helpers;
using GradeBookMicroservice.WebHost.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AuctionTrading.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString(nameof(ApplicationDbContext));

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string for EmailSenderMicroserviceDbContext is not configured.");
            }

            builder.Services.AddNpgsql<ApplicationDbContext>(connectionString, options =>
            {
                options.MigrationsAssembly("AuctionTrading.Infrastructure.EntityFramework");

            });

            builder.Services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Auction trading API",
                        Description = "The Auction trading API provides endpoints for auction management. This API allows you to put lots up for bidding and participate in an auction."
                    });
                });

            builder.Services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseNpgsql(connectionString);
                });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IRepository<Bid, Guid>, EfRepository<Bid, Guid>>();

            builder.Services.AddScoped<IAuctionLotRepository, EfAuctionLotRepository>();
            builder.Services.AddScoped<IAuctionLotsApplicationService, AuctionLotsApplicationService>();

            builder.Services.AddScoped<ISellersRepository, EfSellerRepository>();
            builder.Services.AddScoped<ISellersApplicationService, SellersApplicationService>();
            builder.Services.AddScoped<ISellingApplicationService, SellingApplicationService>();

            builder.Services.AddScoped<ICustomersRepository, EfCustomerRepository>();
            builder.Services.AddScoped<ICustomersApplicationService, CustomersApplicationService>();
            builder.Services.AddScoped<IBidderApplicationService, BidderApplicationService>();

            builder.Services.AddAutoMapper(typeof(RepresentationProfile), typeof(ApplicationProfile));
            //builder.Services.AddAutoMapper(typeof(Program), typeof(GroupMapping));

            builder.Services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseNpgsql(connectionString);
                });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MigrateDatabase<ApplicationDbContext>();

            app.Run();
        }
    }
}
