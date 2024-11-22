using AuctionTrading.Application.Models.Base;
using AuctionTrading.Application.Models.Bid;
using AuctionTrading.Domain.Enums;

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
        string SellerUsername,
        BidModel? LastBid,
        LotStatus Status) : IModel<Guid>;
}
