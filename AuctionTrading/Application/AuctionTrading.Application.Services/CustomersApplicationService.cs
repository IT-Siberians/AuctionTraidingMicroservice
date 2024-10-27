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
        public async Task<IEnumerable<CustomerModel>> GetCustomersAsync(CancellationToken cancellationToken = default)
            => (await repository.GetAllAsync(cancellationToken = default, true)).Select(mapper.Map<CustomerModel>);

        public async Task<CustomerModel?> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var customer = await repository.GetByIdAsync(id, cancellationToken);
            return customer is null ? null : mapper.Map<CustomerModel>(customer);
        }

        public async Task<CustomerModel?> GetCustomerByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            var customer = await repository.GetCustomerByUsernameAsync(username, cancellationToken);
            return customer is null ? null : mapper.Map<CustomerModel>(customer);
        }
        public async Task<bool> CreateCustomerAsync(CreateCustomerModel customerInformation, CancellationToken cancellationToken = default)
        {
            if (await repository.GetByIdAsync(customerInformation.Id, cancellationToken) is not null)
                return false;

            Customer customer = new(customerInformation.Id, new Username(customerInformation.Username));
            return await repository.AddAsync(customer, cancellationToken);
        }

        public async Task<bool> UpdateCustomerAsync(CustomerModel customer, CancellationToken cancellationToken = default)
        {
            var entity = await repository.GetByIdAsync(customer.Id, cancellationToken);
            if (entity is null)
                return false;

            entity = mapper.Map<Customer>(customer);
            return await repository.UpdateAsync(entity, cancellationToken);
        }

        public async Task<bool> DeleteCustomerAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var customer = await repository.GetByIdAsync(id, cancellationToken);
            return customer is null ? false : await repository.DeleteAsync(customer, cancellationToken);
        }
    }
}
