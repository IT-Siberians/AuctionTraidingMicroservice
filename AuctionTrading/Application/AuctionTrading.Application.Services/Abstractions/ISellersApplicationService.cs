using AuctionTrading.Application.Models.Seller;

namespace AuctionTrading.Application.Services.Abstractions
{
    public interface ISellersApplicationService
    {
        Task<SellerModel?> GetSellerByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<SellerModel?> GetSellerByUsernameAsync(string username, CancellationToken cancellationToken);

        Task<IEnumerable<SellerModel>> GetSellersAsync(CancellationToken cancellationToken);

        Task<bool> CreateSellerAsync(CreateSellerModel sellerInformation, CancellationToken cancellationToken);

        Task<bool> UpdateSellerAsync(SellerModel seller, CancellationToken cancellationToken);

        Task<bool> DeleteSellerAsync(Guid id, CancellationToken cancellationToken);
    }
}
