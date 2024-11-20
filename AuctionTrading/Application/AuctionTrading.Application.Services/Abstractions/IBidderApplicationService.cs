using AuctionTrading.Application.Models.Bid;
using AuctionTrading.Common.Enums;
using AuctionTrading.Common.Responses.Base;

namespace AuctionTrading.Application.Services.Abstractions
{
    public interface IBidderApplicationService
    {
        Task<IResponse<BidStatus>> MakeBidAsync(CreateBidModel bidInformation, CancellationToken cancellationToken);
    }
}
