using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Repositories.Abstractions;
using AuctionTrading.Infrastructure.EntityFramework;
using AuctionTrading.Infrastructure.Repositories.Implementations.EF;

namespace AuctionTrading.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddNpgsql<ApplicationDbContext>("Host=localhost;Port=5432;Database=AuctionTradingdb;UserName=postgres;Password=12345", options =>
            {
                options.MigrationsAssembly("AuctionTrading.Infrastructure.EntityFramework");

            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IRepository<Bid, Guid>, EfRepository<Bid, Guid>>();
            //builder.Services.AddScoped<IStudentsApplicationService, StudentsApplicationService>();
            builder.Services.AddScoped<IRepository<AuctionLot, Guid>, EfAuctionLotRepository>();
            //builder.Services.AddScoped<ITeachersApplicationService, TeachersApplicationService>();
            builder.Services.AddScoped<IRepository<Seller, Guid>, EfSellerRepository>();
            //builder.Services.AddScoped<ILessonsApplicationService, LessonsApplicationService>();
            //builder.Services.AddScoped<ITeachingApplicationService, TeachingApplicationService>();
            //builder.Services.AddScoped<IVisitingApplicationService, VisitingApplicationService>();
            builder.Services.AddScoped<IRepository<Customer, Guid>, EfCustomerRepository>();
            //builder.Services.AddScoped<IAssesmentApplicationService, AssesmentApplicationService>();
            //builder.Services.AddAutoMapper(typeof(Program), typeof(GroupMapping));

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

            // app.MigrateDatabase<ApplicationDbContext>();

            app.Run();
        }
    }
}
