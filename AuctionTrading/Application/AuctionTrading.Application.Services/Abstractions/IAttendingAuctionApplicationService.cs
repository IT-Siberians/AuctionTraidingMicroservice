using AuctionTrading.Application.Models.AuctionLot;
using AuctionTrading.Application.Models.Customer;
using AuctionTrading.Common.Enums;

namespace AuctionTrading.Application.Services.Abstractions
{
    public interface IAttendingAuctionApplicationService
    {
        Task<AuctionLotModel?> ViewAuctionLotAsync(ViewAuctionLotDetailsModel auctionLotId);

        Task<BidStatus> BidOnAuctionLotAsync(BidOnAuctionLotModel bidDetails);
    }
}
