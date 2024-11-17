using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Grpc.Core;
using AuctionGrpcClient;
using AuctionTrading.GrpcService.Commands.Base;
using AuctionTrading.GrpcService.Commands;

namespace AuctionTrading.GrpcService.Services
{
    public class TradingService(
        ICommandHandler<PayForLotCommand> payHandler,
        ICommandHandler<RealeaseMoneyCommand> realeaseHandler,
        ICommandHandler<ReserveMoneyCommand> reserveHandler)
            : Trading.TradingClient
    {
        public override async Task<BaseResponseGrpc> PayForLot(PayForLotCommandGrpc request, ServerCallContext context)
        {

            var answer = await payHandler.HandleAsync(query, context.CancellationToken);

            return GetResponse(answer);
        }

        public override async Task<BaseResponseGrpc> RealeaseMoney(RealeaseMoneyCommandGrpc request, ServerCallContext context)
        {
            
            var answer = await realeaseHandler.HandleAsync(query, context.CancellationToken);

            return GetResponse(answer);
        }

        public override async Task<BaseResponseGrpc> ReserveMoney(ReserveMoneyCommandGrpc request, ServerCallContext context)
        {
            var answer = await reserveHandler.HandleAsync(query, context.CancellationToken);

            return GetResponse(answer);
        }

        private static BaseResponseGrpc GetResponse(IAnswer answer)
        {
            if (answer is IOkAnswer okAnswer)
            {
                return GetOkResponse(okAnswer.Message ?? "Ok");
            }
            else if (answer is IBadAnswer badAnswer)
            {
                return GetErrorResponse(badAnswer.ErrorMessage ?? "Error");
            }
            else
            {
                return GetErrorResponse(CommonMessages.UnknownError);
            }
        }

        private static BaseResponseGrpc GetErrorResponse(string errorMessage)
        {
            return new BaseResponseGrpc
            {
                IsError = true,
                Message = errorMessage
            };
        }

        private static BaseResponseGrpc GetOkResponse(string okMessage)
        {
            return new BaseResponseGrpc
            {
                IsError = false,
                Message = okMessage
            };
        }
    }
}
