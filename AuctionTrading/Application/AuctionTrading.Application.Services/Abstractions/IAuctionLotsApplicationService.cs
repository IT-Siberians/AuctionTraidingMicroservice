using AuctionTrading.Application.Models.AuctionLot;

namespace AuctionTrading.Application.Services.Abstractions
{
    public interface IAuctionLotsApplicationService
    {
        Task<AuctionLotModel?> GetAuctionLotByIdAsync(Guid id);

        Task<IEnumerable<AuctionLotModel>> GetAuctionLotsAsync();

        Task<bool> CreateAuctionLotAsync(CreateAuctionLotModel auctionLotInformation);
        
        Task<bool> UpdateAuctionLotAsync(AuctionLotModel auctionLot);
        
        Task<bool> DeleteAuctionLotAsync(Guid id);
    }
}
