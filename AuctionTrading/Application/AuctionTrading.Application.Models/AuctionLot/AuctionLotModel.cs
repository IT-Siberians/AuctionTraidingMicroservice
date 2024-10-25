using AuctionTrading.Application.Models.Base;
using AuctionTrading.Application.Models.Bid;

namespace AuctionTrading.Application.Models.AuctionLot
{
    public record class AuctionLotModel(
        Guid Id,
        string Title,
        string Description,
        decimal StartPrice,
        decimal BidIncrement,
        decimal? RepurchasePrice,
        DateTime StartDate,
        DateTime EndDate,
        Guid SellerId,
        BidModel? LastBid) : IModel<Guid>;
}
