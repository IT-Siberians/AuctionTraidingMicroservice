using AuctionTrading.Application.Models.AuctionLot;
using AuctionTrading.Application.Models.Base;

namespace AuctionTrading.Application.Models.Seller
{
    public class SellerModel : BidderModel
    {
        public required IEnumerable<AuctionLotModel> AuctionedLots { get; init; }
    }
}
