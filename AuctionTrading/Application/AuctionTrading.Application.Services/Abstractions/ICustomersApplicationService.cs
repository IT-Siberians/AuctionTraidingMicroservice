using AuctionTrading.Application.Models.Customer;

namespace AuctionTrading.Application.Services.Abstractions
{
    public interface ICustomersApplicationService
    {
        Task<CustomerModel?> GetCustomerByIdAsync(Guid id);

        Task<IEnumerable<CustomerModel>> GetCustomersAsync();

        Task<bool> CreateCustomerAsync(CreateCustomerModel customerInformation);

        Task<bool> UpdateCustomerAsync(CustomerModel seller);

        Task<bool> DeleteCustomerAsync(Guid id);
    }
}
