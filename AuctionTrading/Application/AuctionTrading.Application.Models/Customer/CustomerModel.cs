using AuctionTrading.Application.Models.AuctionLot;
using AuctionTrading.Application.Models.Base;

namespace AuctionTrading.Application.Models.Customer
{
    public record class CustomerModel(
        Guid Id,
        string Username,
        IEnumerable<AuctionLotModel> ObservableAuctionedLots)
        : BidderModel(Id, Username);
}
