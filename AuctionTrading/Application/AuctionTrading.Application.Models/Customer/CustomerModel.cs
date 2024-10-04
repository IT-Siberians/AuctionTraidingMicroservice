using AuctionTrading.Application.Models.AuctionLot;
using AuctionTrading.Application.Models.Base;

namespace AuctionTrading.Application.Models.Customer
{
    internal class CustomerModel : BidderModel
    {
        public required IEnumerable<AuctionLotModel> ObservableAuctionedLots { get; init; }

    }
}
