using AuctionTrading.Domain.Entities;

namespace AuctionTrading.Domain.Exceptions
{
    public class AuctionLotDoesNotBelongToSellerException(Seller seller, AuctionLot lot)
        : InvalidOperationException($"The auction lot {lot.Title} is not in the seller's lot sequence (seller {seller.Username}, lot id = {lot.Id}).")
    {
        public Seller Seller => seller;
        public AuctionLot Lot => lot;
    }
}
