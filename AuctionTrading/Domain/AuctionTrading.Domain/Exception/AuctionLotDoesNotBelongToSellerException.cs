using AuctionTrading.Domain.Entities;

namespace AuctionTrading.Domain.Exception
{
    public class AuctionLotDoesNotBelongToSellerException(Seller seller, AuctionLot lot)
        : InvalidOperationException($"The auction lot {lot.Title} is not in the seller's lot sequence (seller's id = {seller.Id}).")
    {
        public Seller Seller => seller;
        public AuctionLot Lot => lot;
    }
}
