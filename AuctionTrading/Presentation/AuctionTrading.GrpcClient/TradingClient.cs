using AuctionGrpcClient;

namespace AuctionTrading.GrpcClient
{
    public interface ITradingClient
    {
        Task<BaseResponseGrpc> PayForLotAsync(PayForLotCommandGrpc request, CancellationToken cancellationToken);
        Task<BaseResponseGrpc> RealeaseMoneyAsync(RealeaseMoneyCommandGrpc request, CancellationToken cancellationToken);
        Task<BaseResponseGrpc> ReserveMoney(ReserveMoneyCommandGrpc request, CancellationToken cancellationToken);

    }
    public class TradingClient : ITradingClient
    {
        private readonly Trading.TradingClient _client;

        public TradingClient(Trading.TradingClient client)
        {
            _client = client;
        }

        public async Task<BaseResponseGrpc> PayForLotAsync(PayForLotCommandGrpc request, CancellationToken cancellationToken)
        {
            return await _client.PayForLotAsync(request, cancellationToken: cancellationToken);
        }

        public async Task<BaseResponseGrpc> RealeaseMoneyAsync(RealeaseMoneyCommandGrpc request, CancellationToken cancellationToken)
        {
            return await _client.RealeaseMoneyAsync(request, cancellationToken: cancellationToken);
        }

        public async Task<BaseResponseGrpc> ReserveMoney(ReserveMoneyCommandGrpc request, CancellationToken cancellationToken)
        {
            return await _client.ReserveMoneyAsync(request, cancellationToken: cancellationToken);
        }
    }
}
