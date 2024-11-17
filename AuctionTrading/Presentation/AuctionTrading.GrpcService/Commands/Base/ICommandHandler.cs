namespace AuctionTrading.GrpcService.Commands.Base
{
    public interface ICommandHandler<TCommand>
    : IHandler<TCommand, IAnswer>
        where TCommand : class;
}
