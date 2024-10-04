using AuctionTrading.Domain.Entities;

namespace AuctionTrading.Domain.Repositories.Abstractions
{
    public interface ISellersRepository : IRepository<Seller, Guid>
    {
        Task<Seller?> GetSellerByUsernameAsync(string username);
    }
}
