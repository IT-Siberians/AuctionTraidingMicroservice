using AuctionTrading.Application.Models.Bid;
using AuctionTrading.Common.Enums;

namespace AuctionTrading.Application.Services.Abstractions
{
    public interface IBidderApplicationService
    {
        Task<BidStatus> MakeBidAsync(CreateBidModel bidInformation, CancellationToken cancellationToken);
    }
}
