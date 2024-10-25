namespace AuctionTrading.Application.Models.Base
{
    public interface IBidderCreateModel<out TId> where TId : struct, IEquatable<TId>
    {
        public TId Id { get; }
        public string Username { get; }
    }
}
