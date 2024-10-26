using AuctionTrading.Application.Models.AuctionLot;

namespace AuctionTrading.Application.Services.Abstractions
{
    public interface IAuctionLotsApplicationService
    {
        Task<AuctionLotModel?> GetAuctionLotByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IEnumerable<AuctionLotModel>> GetAuctionLotsAsync(CancellationToken cancellationToken);

        Task<IEnumerable<AuctionLotModel>> GetAuctionLotsByEndDateAsync(DateTime endDateUtc, CancellationToken cancellationToken);

        Task<bool> CreateAuctionLotAsync(CreateAuctionLotModel auctionLotInformation, CancellationToken cancellationToken);

        Task<bool> UpdateAuctionLotAsync(AuctionLotModel auctionLot, CancellationToken cancellationToken);

        Task<bool> DeleteAuctionLotAsync(Guid id, CancellationToken cancellationToken);
    }
}
