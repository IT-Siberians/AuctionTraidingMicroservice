using AuctionTrading.Common.Enums;
using AuctionTrading.Common.Responses.Base;

namespace AuctionTrading.Common.Responses
{
    public class BidResponse(BidStatus result, string? message = null) : IResponse<BidStatus>
    {
        public BidStatus Result { get; } = result;

        public string? Message { get; } = message;
    }
}
