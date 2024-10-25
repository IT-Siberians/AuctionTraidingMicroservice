using AuctionTrading.Application.Models.Customer;
using AuctionTrading.Application.Services.Abstractions;
using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Repositories.Abstractions;
using AuctionTrading.Domain.ValueObjects;
using AutoMapper;

namespace AuctionTrading.Application.Services
{
    public class CustomersApplicationService(ICustomersRepository repository, IMapper mapper) : ICustomersApplicationService
    {
        public async Task<IEnumerable<CustomerModel>> GetCustomersAsync()
            => (await repository.GetAllAsync()).Select(mapper.Map<CustomerModel>);

        public async Task<CustomerModel?> GetCustomerByIdAsync(Guid id)
        {
            var customer = await repository.GetByIdAsync(id);
            return customer is null ? null : mapper.Map<CustomerModel>(customer);
        }

        public async Task<CustomerModel?> GetCustomerByUsernameAsync(string username)
        {
            var customer = await repository.GetCustomerByUsernameAsync(username);
            return customer is null ? null : mapper.Map<CustomerModel>(customer);
        }
        public async Task<bool> CreateCustomerAsync(CreateCustomerModel customerInformation)
        {
            Customer customer = new(customerInformation.Id, new Username(customerInformation.Username));
            return await repository.AddAsync(customer);
        }

        public async Task<bool> UpdateCustomerAsync(CustomerModel customer)
        {
            var entity = await repository.GetByIdAsync(customer.Id);
            if (entity is null)
                return false;
            entity = mapper.Map<Customer>(customer);
            return await repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteCustomerAsync(Guid id)
        {
            var customer = await repository.GetByIdAsync(id);
            return customer is null ? false : await repository.DeleteAsync(customer);
        }
    }
}
