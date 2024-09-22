using AuctionTrading.Domain.Entities;

namespace AuctionTrading.Domain.Exceptions
{
    public class CompletedNotActiveAuctionLotException(AuctionLot lot)
        : InvalidOperationException($"Can't completed an inactive lot (id = {lot.Id}).")
    {
        public AuctionLot Lot => lot;
    }
}
