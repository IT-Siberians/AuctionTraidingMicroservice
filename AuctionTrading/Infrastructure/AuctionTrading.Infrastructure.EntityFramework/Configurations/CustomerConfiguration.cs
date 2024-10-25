using AuctionTrading.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AuctionTrading.Domain.ValueObjects;

namespace AuctionTrading.Infrastructure.EntityFramework.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Username)
                .IsRequired()
                .HasConversion(username => username.Value, str => new Username(str))
                .HasMaxLength(30);
            builder.Ignore(x => x.ObservableAuctionLots);
        }
    }
}
