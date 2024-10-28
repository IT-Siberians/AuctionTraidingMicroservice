using AuctionTrading.Application.Models.AuctionLot;

namespace AuctionTrading.Application.Services.Abstractions
{
    public interface ISellingApplicationService
    {
        Task<bool> CancelAuctionLotAsync(AuctionLotModel information, CancellationToken cancellationToken);
    }
}
