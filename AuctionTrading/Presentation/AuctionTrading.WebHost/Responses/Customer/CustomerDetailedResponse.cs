using AuctionTrading.Application.Models.AuctionLot;

namespace AuctionTrading.WebHost.Responses.Customer
{
    public record class CustomerDetailedResponse(
        Guid Id,
        string Username,
        IEnumerable<AuctionLotModel> ObservedLots);
}
