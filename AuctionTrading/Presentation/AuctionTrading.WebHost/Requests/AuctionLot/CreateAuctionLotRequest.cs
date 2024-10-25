namespace AuctionTrading.WebHost.Requests.AuctionLot
{
    public record class CreateAuctionLotRequest(
       Guid Id,
       string Title,
       string Description,
       decimal StartPrice,
       decimal BidIncrement,
       DateTime StartDate,
       DateTime EndDate,
       Guid SellerId);
}
