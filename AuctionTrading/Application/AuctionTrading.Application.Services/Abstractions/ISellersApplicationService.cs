using AuctionTrading.Application.Models.AuctionLot;
using AuctionTrading.Application.Models.Base;
using AuctionTrading.Application.Models.Seller;

namespace AuctionTrading.Application.Services.Abstractions
{
    public interface ISellersApplicationService
    {
        Task<IEnumerable<AuctionLotModel>> GetAllAuctionedLotsAsync();
        Task<AuctionLotModel?> GetAuctionLotByIdAsync(Guid auctionLotId);
        Task<bool> ChangeNameAsync(BidderChangeNameModel bidderChangeName);
        Task<bool> CancelAuctionLotAsync(CancelAuctionLotModel cancelAuctionLot);
        Task<bool> DeleteSellerByIdAsync(Guid Id);

    }
}
