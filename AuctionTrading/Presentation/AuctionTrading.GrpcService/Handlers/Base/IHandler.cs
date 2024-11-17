namespace AuctionTrading.GrpcService.Handlers.Base
{
    public interface IHandler<TInput, TOutput> where TInput : class
    {
        Task<TOutput> HandleAsync(TInput input, CancellationToken cancellationToken = default);
    }
}
