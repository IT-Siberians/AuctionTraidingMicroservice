using AuctionTrading.Application.Models.Bid;
using AuctionTrading.Application.Models.Customer;
using AuctionTrading.Application.Models.Seller;
using AuctionTrading.Common.Enums;

namespace AuctionTrading.Application.Services.Abstractions
{
    public interface IBidderApplicationService
    {
        Task<BidStatus> MakeBidAsync(CreateBidModel bidInformation);

        Task<bool> CancelAuctionLotAsync(CancelAuctionLotModel information);
    }
}
