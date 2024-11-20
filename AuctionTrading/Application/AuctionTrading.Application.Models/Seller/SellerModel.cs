using AuctionTrading.Application.Models.AuctionLot;
using AuctionTrading.Application.Models.Base;

namespace AuctionTrading.Application.Models.Seller
{
    public record class SellerModel(Guid Id, string Username) : BidderModel(Id, Username) 
    {
        public IEnumerable<AuctionLotModel> AuctionedLots { get; init; }
    }
}
