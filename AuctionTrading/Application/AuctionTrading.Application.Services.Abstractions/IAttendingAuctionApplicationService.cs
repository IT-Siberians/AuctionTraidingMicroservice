namespace AuctionTrading.Application.Services.Abstractions
{
    public interface IAttendingAuctionApplicationService
    {
        Task<AuctionLotModel?> ViewAuctionLotAsync(ViewAuctionLotDetailsModel auctionLotId);
    }
}
