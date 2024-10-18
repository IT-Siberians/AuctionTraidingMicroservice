namespace AuctionTrading.Application.Models.Base
{
    public abstract record class BidderModel(Guid Id, string Username) 
        : IModel<Guid>;
}
