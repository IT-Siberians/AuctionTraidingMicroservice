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
                .HasConversion(username => username.Value, str => new Username(str))
                .HasMaxLength(30);
            builder.HasMany<AuctionLot>("_auctionLots")
                .WithOne(x => x.Seller)
                .HasForeignKey("SellerId")
                .HasPrincipalKey(x => x.Id);
            builder.Ignore(x => x.ActiveAuctionLots);
        }
    }
}
