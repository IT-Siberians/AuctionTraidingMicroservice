using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AuctionTrading.Domain.Entities;

namespace AuctionTrading.Infrastructure.EntityFramework.Configurations
{
    public class BidConfiguration : IEntityTypeConfiguration<Bid>
    {
        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.CreationTime).IsRequired().HasConversion
            (
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
            );
            builder.HasOne(x => x.AuctionLot).WithMany("_bids");
            builder.HasOne(x => x.Customer).WithMany();
            builder.Navigation(x => x.AuctionLot).AutoInclude();
            builder.Navigation(x => x.Customer).AutoInclude();
        }
    }
}
