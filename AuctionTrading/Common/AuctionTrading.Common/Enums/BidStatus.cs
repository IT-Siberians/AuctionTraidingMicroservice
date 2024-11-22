namespace AuctionTrading.Common.Enums
{
    public enum BidStatus
    {
        Success,
        FaultedIncorrectBid,
        FaultedCreateBidOnYourLot,
        FaultedLotNotActive,
        FaultedLotWasCancel,
        FaultedLotWasPurchased,
        FaultedCustomerNotFound,
        FaultedLotNotFound,
        FaultedMoneyIsNotFrozen,
        FaultedPayForLot,
        FaultedNotRealeaseMoney

    }
}
