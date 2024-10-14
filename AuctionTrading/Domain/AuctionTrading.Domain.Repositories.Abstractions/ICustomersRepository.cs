using AuctionTrading.Domain.Entities;

namespace AuctionTrading.Domain.Repositories.Abstractions
{
    public interface ICustomersRepository : IRepository<Customer, Guid>
    {
        Task<Customer?> GetCustomerByUsernameAsync(string username);

    }
}
