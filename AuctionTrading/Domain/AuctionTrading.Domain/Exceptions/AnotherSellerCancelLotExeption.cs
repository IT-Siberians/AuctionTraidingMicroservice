using AuctionTrading.Domain.Entities;

namespace AuctionTrading.Domain.Exceptions
{
    public class AnotherSellerCancelLotException(AuctionLot auctionLot, Seller seller)
        : InvalidOperationException($"The seller {seller.Username} can't cancel the {auctionLot.Title} auction lot owned by the seller  {auctionLot.Seller.Username} (lot id = {auctionLot.Id}).")
    {
        public AuctionLot AuctionLot => auctionLot;
        public Seller Seller => seller;
    }
}
