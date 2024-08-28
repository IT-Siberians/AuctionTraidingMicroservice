using AuctionTrading.Domain.Entities;

namespace AuctionTrading.Domain.Exception
{
    public class AnotherSellerCancelLotException(AuctionLot auctionLot, Seller seller)
        : InvalidOperationException($"The seller {seller.Username} can't cancel the {auctionLot.Title} auction lot.")
    {
        public AuctionLot AuctionLot => auctionLot;
        public Seller Seller => seller;
    }
}
