using AuctionTrading.Application.Models.Base;
using AuctionTrading.Application.Models.Bid;

namespace AuctionTrading.Application.Models.AuctionLot
{
    public class AuctionLotModel : IModel<Guid>
    {
        public required Guid Id { get; init; }

        public required string Title { get; init; }

        public required string Description { get; init; }

        public required decimal StartPrice { get; init; }

        public required decimal BidIncrement { get; init; }

        public decimal? RepurchasePrice { get; }

        public required DateTime StartDate { get; init; }

        public required DateTime EndDate { get; init; }

        public required Guid SellerId { get; init; }

        public BidModel? LastBid { get; }
    }
}
