namespace AuctionTrading.Common.Enums
{
    public enum BidStatus
    {
        Success,
        FaultedIncorrectBid,
        FaultedCreateBidOnYourLot,
        FaultedLotWasCancel,
        FaultedLotWasPurchased
    }
}
