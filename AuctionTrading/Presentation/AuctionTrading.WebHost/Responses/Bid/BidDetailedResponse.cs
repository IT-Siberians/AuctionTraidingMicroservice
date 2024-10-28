namespace AuctionTrading.WebHost.Responses.Bid
{
    public record class BidDetailedResponse(
        Guid Id,
        DateTime CreationTime,
        decimal Amount,
        Guid AuctionLotId,
        Guid CustomerId);
}