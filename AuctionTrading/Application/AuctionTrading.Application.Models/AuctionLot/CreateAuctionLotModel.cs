using AuctionTrading.Application.Models.Base;

namespace AuctionTrading.Application.Models.AuctionLot
{
    public record class CreateAuctionLotModel(
       Guid Id,
       string Title,
       string Description,
       decimal StartPrice,
       decimal BidIncrement,
       decimal? RepurchasePrice,
       DateTime StartDate,
       DateTime EndDate,
       Guid SellerId) : ICreateModel;
}
