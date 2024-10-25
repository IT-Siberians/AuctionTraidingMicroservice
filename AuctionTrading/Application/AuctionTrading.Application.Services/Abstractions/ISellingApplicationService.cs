using AuctionTrading.Application.Models.Seller;

namespace AuctionTrading.Application.Services.Abstractions
{
    public interface ISellingApplicationService
    {
        Task<bool> CancelAuctionLotAsync(CancelAuctionLotModel information);
    }
}
