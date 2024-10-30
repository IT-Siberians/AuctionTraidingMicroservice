using AuctionTrading.Application.Models.AuctionLot;
using AuctionTrading.Application.Models.Base;

namespace AuctionTrading.Application.Models.Customer
{
    public record class CustomerModel(Guid Id, string Username) : BidderModel(Id, Username)
    {
        public IEnumerable<AuctionLotModel> ObservedAuctionLots { get; init; }
    }
}
