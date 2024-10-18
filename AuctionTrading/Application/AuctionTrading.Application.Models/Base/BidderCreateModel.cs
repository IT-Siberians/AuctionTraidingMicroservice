namespace AuctionTrading.Application.Models.Base
{
    public abstract record class BidderCreateModel(Guid Id, string Username) 
        : IBidderCreateModel<Guid>;
}
