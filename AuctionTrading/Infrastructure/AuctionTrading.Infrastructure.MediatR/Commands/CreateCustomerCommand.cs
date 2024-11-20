using MediatR;

namespace AuctionTrading.Infrastructure.MediatR.Commands
{
    public class CreateCustomerCommand<TModel> : IRequest<bool> where TModel : class
    {
        public TModel Message { get; }
        public CreateCustomerCommand(TModel message) => Message = message;
    }
}
