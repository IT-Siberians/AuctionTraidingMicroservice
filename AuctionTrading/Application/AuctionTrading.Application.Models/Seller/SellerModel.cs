using AuctionTrading.Application.Models.AuctionLot;
using AuctionTrading.Application.Models.Base;

namespace AuctionTrading.Application.Models.Seller
{
    public class SellerModel : PersonModel
    {
        public required IEnumerable<AuctionLotModel> AuctionedLots { get; init; }
    }
}
