using AuctionTrading.Application.Models.Customer;

namespace AuctionTrading.Application.Services.Abstractions
{
    public interface IChangeWatchListOfAuctionLotsApplicationServices
    {
        Task<bool> AddAuctionLotToWatchListOfAsync(AddAuctionLotToWatchListModel auctionLotId);

        Task<bool> RemoveAuctionLotFromWatchListOfAsync(RemoveAuctionLotFromWatchListModel auctionLotId);
    }
}
