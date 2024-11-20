using AuctionTrading.Application.Models.Customer;
using AuctionTrading.Application.Services.Abstractions;
using AuctionTrading.Infrastructure.MediatR.Commands;
using AutoMapper;
using MediatR;
using Otus.QueueDto.User;

namespace AuctionTrading.Infrastructure.MediatR.Handlers
{
    public class CreateCustomerHandler(ICustomersApplicationService customerService, IMapper mapper) : IRequestHandler<CreateCustomerCommand<CreateUserEvent>, bool>
    {
        public async Task<bool> Handle(CreateCustomerCommand<CreateUserEvent> request, CancellationToken cancellationToken)
        {
            return await customerService.CreateCustomerAsync(mapper.Map<CreateCustomerModel>(request.Message), cancellationToken) != null;
        }
    }
}
