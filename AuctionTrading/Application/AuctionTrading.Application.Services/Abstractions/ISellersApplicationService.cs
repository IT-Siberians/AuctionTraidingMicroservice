using AuctionTrading.Application.Models.Seller;

namespace AuctionTrading.Application.Services.Abstractions
{
    public interface ISellersApplicationService
    {
        Task<SellerModel?> GetSellerByIdAsync(Guid id);

        Task<SellerModel?> GetSellerByUsernameAsync(string username);

        Task<IEnumerable<SellerModel>> GetSellersAsync();
        
        Task<bool> CreateSellerAsync(CreateSellerModel sellerInformation);
        
        Task<bool> UpdateSellerAsync(SellerModel seller);
        
        Task<bool> DeleteSellerAsync(Guid id);
    }
}
