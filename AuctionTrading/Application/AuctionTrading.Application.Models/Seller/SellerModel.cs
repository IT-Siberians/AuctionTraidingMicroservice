using AuctionTrading.Application.Models.AuctionLot;
using AuctionTrading.Application.Models.Base;

namespace AuctionTrading.Application.Models.Seller
{
    public record class SellerModel(
        Guid Id,
        string Username,
        IEnumerable<AuctionLotModel> AuctionedLots)
        : BidderCreateModel(Id, Username);
}
