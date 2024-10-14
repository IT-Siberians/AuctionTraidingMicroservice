namespace AuctionTrading.Application.Models.Base
{
    public abstract class BidderModel : IModel<Guid>
    {
        public required Guid Id { get; init; }
        public required string Username { get; init; }

    }
}
