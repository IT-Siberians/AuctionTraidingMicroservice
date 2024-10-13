using AuctionTrading.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AuctionTrading.Domain.ValueObjects;

namespace AuctionTrading.Infrastructure.EntityFramework.Configurations
{
    public class SellerConfiguration : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Username)
                .IsRequired()
                .HasConversion(username => username.Value, username => new Username(username))
                .HasMaxLength(30);
            builder.HasMany("_auctionLots").WithOne();
            builder.Ignore(x => x.ActiveAuctionLots);
        }
    }
}
