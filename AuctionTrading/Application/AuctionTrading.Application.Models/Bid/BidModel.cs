using AuctionTrading.Application.Models.Base;

namespace AuctionTrading.Application.Models.Bid
{
    public record class BidModel(
        Guid Id,
        DateTime CreationTime,
        decimal Amount,
        Guid AuctionLotId,
        Guid CustomerId)
        : IModel<Guid>;

}
