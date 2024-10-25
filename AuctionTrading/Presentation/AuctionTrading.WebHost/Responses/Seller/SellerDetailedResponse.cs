using AuctionTrading.Application.Models.AuctionLot;

namespace AuctionTrading.WebHost.Responses.Seller
{
    public record class SellerDetailedResponse(Guid Id, string Username, IEnumerable<AuctionLotModel> AuctionedLots);
}
