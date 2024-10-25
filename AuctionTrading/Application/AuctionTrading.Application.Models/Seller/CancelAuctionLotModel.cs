namespace AuctionTrading.Application.Models.Seller
{
    public record class CancelAuctionLotModel(Guid SellerId, Guid AuctionLotId);
}
