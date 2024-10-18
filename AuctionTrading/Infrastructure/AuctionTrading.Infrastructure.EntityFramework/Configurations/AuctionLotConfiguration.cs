using AuctionTrading.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AuctionTrading.Domain.ValueObjects;

namespace AuctionTrading.Infrastructure.EntityFramework.Configurations
{
    public class AuctionLotConfiguration : IEntityTypeConfiguration<AuctionLot>
    {
        public void Configure(EntityTypeBuilder<AuctionLot> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Title)
                .IsRequired()
                .HasConversion(title => title.Value, str => new Title(str))
                .HasMaxLength(50);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.Description)
                .IsRequired()
                .HasConversion(description => description.Value, str => new Description(str));
            builder.Property(x => x.StartPrice)
                .IsRequired()
                .HasConversion(startPrice => startPrice.Value, d => new Money(d));
            builder.Property(x => x.BidIncrement)
                .IsRequired()
                .HasConversion(bidIncrement => bidIncrement.Value, d => new Money(d));
            builder.Property(x => x.RepurchasePrice)
                .IsRequired(false)
                .HasConversion(repurchasePrice => repurchasePrice!.Value, d => new Money(d));
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
            builder.HasMany<Bid>("_bids").WithOne(x => x.AuctionLot);
            builder.Ignore(x => x.IsActive);
            builder.Ignore(x => x.LastBid);
        }
    }
}
