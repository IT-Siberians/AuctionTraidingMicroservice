using AuctionTrading.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AuctionTrading.Infrastructure.EntityFramework.Configurations
{
    public class AuctionLotConfiguration : IEntityTypeConfiguration<AuctionLot>
    {
        public void Configure(EntityTypeBuilder<AuctionLot> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.StartPrice).IsRequired();
            builder.Property(x => x.BidIncrement).IsRequired();
            builder.Property(x => x.RepurchasePrice).IsRequired(false);
            builder.Property(x => x.StartDate).IsRequired().HasConversion
            (
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
            );
            builder.Property(x => x.EndDate).IsRequired().HasConversion
            (
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
            );
            builder.HasOne(x => x.Seller).WithMany("_auctionLots");
            builder.HasMany("_bids").WithOne();
            builder.Navigation(x => x.Seller).AutoInclude();
            builder.Navigation(x => x.LastBid).AutoInclude(); // Вот тут не уверена!!!
            builder.Ignore(x => x.IsActive);
            builder.Ignore(x => x.LastBid);
        }
    }
}
