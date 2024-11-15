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
using FluentValidation.AspNetCore;
using AuctionTrading.Infrastructure.Queues.Implementations.Producers;
using FluentValidation;
using MassTransit;
using AuctionTrading.Infrastructure.RabbitMQ;
using AuctionTrading.Common.Infrastructure.Queues.Abstraction;
using Otus.QueueDto.Lot;
using MediatR;
using System.Reflection;
using Otus.QueueDto.User;
using AuctionTrading.Infrastructure.MediatR.Handlers;
using AuctionTrading.Infrastructure.MediatR.Commands;
using AuctionTrading.Infrastructure.Queues.Implementations.Consumers;
using AuctionTrading.Infrastructure.MediatR.Mapper;

namespace AuctionTrading.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var dbConnectionString = builder.Configuration.GetConnectionString(nameof(ApplicationDbContext));

            if (string.IsNullOrEmpty(dbConnectionString))
            {
                throw new InvalidOperationException("Connection string for AuctionTradingMicroserviceDbContext is not configured.");
            }

            var rmqConnectionString = builder.Configuration.GetConnectionString(nameof(RabbitMqConfig));

            if (string.IsNullOrEmpty(rmqConnectionString))
            {
                throw new InvalidOperationException("Connection string for RabbitMqConfig is not configured.");
            }

            builder.Services.AddNpgsql<ApplicationDbContext>(dbConnectionString, options =>
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
                    options.UseNpgsql(dbConnectionString);
                });


            builder.Services.AddAutoMapper(typeof(QueueProfile), typeof(PresentationProfile), typeof(ApplicationProfile));


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.Configure<RabbitMqConfig>(builder.Configuration.GetSection(nameof(RabbitMqConfig)));

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

            builder.Services.AddTransient<IProducerService<BidPerLotEvent>, Producer<BidPerLotEvent>>();
            builder.Services.AddTransient<IProducerService<WonLotEvent>, Producer<WonLotEvent>>();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            builder.Services.AddTransient<IRequestHandler<CreateSellerCommand<CreateUserEvent>, bool>, CreateSellerHandler>();
            builder.Services.AddTransient<IRequestHandler<CreateCustomerCommand<CreateUserEvent>, bool>, CreateCustomerHandler>();

            builder.Services.AddHealthChecks()
                            .AddNpgSql(dbConnectionString)
                            .AddRabbitMQ(rabbitConnectionString: rmqConnectionString)
                            .AddDbContextCheck<ApplicationDbContext>();

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<CreateUserConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(rmqConnectionString));
                    cfg.ReceiveEndpoint($"{nameof(CreateUserEvent)}.AuctionTrading", e =>
                    {
                        e.ConfigureConsumer<CreateUserConsumer>(context);
                    });
                    cfg.ConfigureEndpoints(context);
                    cfg.UseMessageRetry(r =>
                    {
                        r.Interval(3, TimeSpan.FromSeconds(10));
                    });
                });
            });




            builder.Services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseNpgsql(dbConnectionString);
                });

            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.AddFluentValidationAutoValidation();



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
