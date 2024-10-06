using AuctionTrading.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AuctionTrading.Infrastructure.EntityFramework.Configurations
{
    public class SellerConfiguration : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Username).IsRequired().HasMaxLength(30);
            builder.HasMany("_auctionLots").WithOne();
            builder.HasMany("_bids").WithOne();
            builder.Navigation(x => x.ActiveAuctionLots).AutoInclude(); // Вот тут не уверена!!!
            builder.Ignore(x => x.ActiveAuctionLots);
        }
    }
}
