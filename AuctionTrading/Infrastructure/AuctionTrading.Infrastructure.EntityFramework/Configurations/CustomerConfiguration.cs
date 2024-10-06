using AuctionTrading.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AuctionTrading.Infrastructure.EntityFramework.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Username).IsRequired().HasMaxLength(30);
            builder.HasMany(x => x.ObservableAuctionLots).WithMany();
            builder.Navigation(x => x.ObservableAuctionLots);
        }
    }
}
