namespace AuctionTrading.Application.Models.Seller
{
    public class CancelAuctionLotModel
    {
        public required Guid SellerId { get; init; }

        public required Guid AuctionLotId { get; init; }
    }
}
