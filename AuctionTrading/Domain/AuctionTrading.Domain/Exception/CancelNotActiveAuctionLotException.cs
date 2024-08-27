using AuctionTrading.Domain.Entities;

namespace AuctionTrading.Domain.Exception
{
    public class CancelNotActiveAuctionLotException(AuctionLot lot)
        : InvalidOperationException($"Can't cancel an inactive lot (id = {lot.Id}).")
    {
        public AuctionLot Lot => lot;
    }
}
