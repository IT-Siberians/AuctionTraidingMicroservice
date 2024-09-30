using AuctionTrading.Application.Models.AuctionLot;
using AuctionTrading.Application.Models.Base;

namespace AuctionTrading.Application.Models.Customer
{
    internal class CustomerModel : PersonModel
    {
        public required IEnumerable<AuctionLotModel> ObservableAuctionedLots { get; init; }

    }
}
