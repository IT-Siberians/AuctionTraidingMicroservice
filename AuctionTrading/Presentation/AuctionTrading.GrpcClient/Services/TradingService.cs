using AuctionTrading.GrpcApi;
using AuctionTrading.GrpcClient;
using Grpc.Core;

namespace AuctionTrading.GrpcClient.Services
{
    public class TradingService
    {
        private readonly Trading.TradingClient _client;

        public TradingService(Trading.TradingClient client)
        {
            _client = client;
        }

        public async Task<BaseResponseGrpc> PayForLotAsync(PayForLotRequestGrpc request, CancellationToken cancellationToken)
        {
            return await _client.PayForLotAsync(request, null, null, cancellationToken); 
        }

        public async Task<BaseResponseGrpc> RealeaseMoneyAsync(RealeaseMoneyRequestGrpc request, CancellationToken cancellationToken)
        {
            return await _client.RealeaseMoneyAsync(request, null, null, cancellationToken);
        }

        public async Task<BaseResponseGrpc> ReserveMoney(ReserveMoneyRequestGrpc request, CancellationToken cancellationToken)
        {
            return await _client.ReserveMoneyAsync(request, null, null, cancellationToken);
        }
    }
}
