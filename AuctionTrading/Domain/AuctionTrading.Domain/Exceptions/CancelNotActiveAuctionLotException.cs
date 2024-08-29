using AuctionTrading.Domain.Entities;

namespace AuctionTrading.Domain.Exceptions
{
    public class CancelNotActiveAuctionLotException(AuctionLot lot)
        : InvalidOperationException($"Can't cancel an inactive lot (id = {lot.Id}).")
    {
        public AuctionLot Lot => lot;
    }
}
