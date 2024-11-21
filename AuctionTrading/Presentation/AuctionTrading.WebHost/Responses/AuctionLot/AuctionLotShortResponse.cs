using AuctionTrading.WebHost.Responses.Bid;

namespace AuctionTrading.WebHost.Responses.AuctionLot
{
    public record class AuctionLotShortResponse(
       Guid Id,
       string Title,
       decimal StartPrice,
       DateTime EndDate,
       string SellerUsername,
       BidDetailedResponse? LastBid);
}
