using MediatR;

namespace AuctionTrading.Infrastructure.MediatR.Commands
{
    public class CreateSellerCommand<TModel> : IRequest<bool>
    {
        public TModel Message { get; }
        public CreateSellerCommand(TModel message) => Message = message;
    }
}
