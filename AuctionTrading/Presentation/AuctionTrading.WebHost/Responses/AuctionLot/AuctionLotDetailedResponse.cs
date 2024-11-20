using AuctionTrading.WebHost.Responses.Bid;

namespace AuctionTrading.WebHost.Responses.AuctionLot
{
    public record class AuctionLotDetailedResponse(
       Guid Id,
       string Title,
       string Description,
       decimal StartPrice,
       decimal BidIncrement,
       decimal? RepurchasePrice,
       DateTime StartDate,
       DateTime EndDate,
       BidDetailedResponse? LastBid,
       Guid SellerId);
}
