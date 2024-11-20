namespace AuctionTrading.WebHost.Requests.Bid
{
    public record class CreateBidRequest(decimal Amount, Guid CustomerId, Guid AuctionLotId);
}
