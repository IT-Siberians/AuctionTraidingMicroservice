namespace AuctionTrading.Application.Models.Seller
{
    public class CancelLotModel
    {
        public required Guid SellerId { get; init; }

        public required Guid AuctionLotId { get; init; }
    }
}
