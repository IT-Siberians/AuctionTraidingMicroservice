namespace AuctionTrading.WebHost.Requests.AuctionLot
{
    public record class CreateAuctionLotRequest(
       Guid Id,
       string Title,
       string Description,
       decimal StartPrice,
       decimal BidIncrement,
       decimal? RepurchasePrice,
       DateTime StartDate,
       DateTime EndDate,
       Guid SellerId);
}
