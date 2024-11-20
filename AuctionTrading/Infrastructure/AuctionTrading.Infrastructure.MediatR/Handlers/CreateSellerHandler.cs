using AuctionTrading.Application.Models.Seller;
using AuctionTrading.Application.Services.Abstractions;
using AuctionTrading.Infrastructure.MediatR.Commands;
using AutoMapper;
using MediatR;
using Otus.QueueDto.User;

namespace AuctionTrading.Infrastructure.MediatR.Handlers
{
    public class CreateSellerHandler(ISellersApplicationService sellerService, IMapper mapper) : IRequestHandler<CreateSellerCommand<CreateUserEvent>, bool>
    {
        public async Task<bool> Handle(CreateSellerCommand<CreateUserEvent> request, CancellationToken cancellationToken)
        {
            return await sellerService.CreateSellerAsync(mapper.Map<CreateSellerModel>(request.Message), cancellationToken) != null;
        }
    }
}
