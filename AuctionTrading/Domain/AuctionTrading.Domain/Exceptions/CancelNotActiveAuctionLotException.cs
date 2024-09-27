using AuctionTrading.Domain.Entities;

namespace AuctionTrading.Domain.Exceptions
{
    public class CancelNotActiveAuctionLotException(AuctionLot lot)
        : InvalidOperationException($"Can't cancel an inactive lot {lot.Title} (id = {lot.Id}).")
    {
        public AuctionLot Lot => lot;
    }
}
