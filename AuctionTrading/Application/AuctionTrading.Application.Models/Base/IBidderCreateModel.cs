namespace AuctionTrading.Application.Models.Base
{
    public interface IBidderCreateModel<out TId> where TId : struct
    {
        public TId Id { get; }
        public string Name { get; }
    }
}
