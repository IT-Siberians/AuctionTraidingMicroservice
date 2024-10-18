using AuctionTrading.Application.Models.Base;

namespace AuctionTrading.Application.Models.Bid
{
    public record class CreateBidModel(decimal Amount, Guid AuctionLotId, Guid CustomerId)
        : ICreateModel;
}
