using AuctionTrading.Application.Models.Customer;
using AuctionTrading.Domain.Entities;

namespace AuctionTrading.Application.Services.Abstractions
{
    public interface ICustomersApplicationService
    {
        Task<CustomerModel?> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<CustomerModel?> GetCustomerByUsernameAsync(string username, CancellationToken cancellationToken);

        Task<IEnumerable<CustomerModel>> GetCustomersAsync(CancellationToken cancellationToken);

        Task<bool> CreateCustomerAsync(CreateCustomerModel customerInformation, CancellationToken cancellationToken);

        Task<bool> UpdateCustomerAsync(CustomerModel customer, CancellationToken cancellationToken);

        Task<bool> DeleteCustomerAsync(Guid id, CancellationToken cancellationToken);
    }
}
