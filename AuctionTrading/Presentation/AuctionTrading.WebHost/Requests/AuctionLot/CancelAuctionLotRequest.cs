namespace AuctionTrading.WebHost.Requests.AuctionLot
{
    public record class CancelAuctionLotRequest(Guid SellerId, Guid AuctionLotId);
}
