namespace AuctionTrading.Common.Responses.Base
{
    public interface IResponse
    {
        string? Message { get; }
    }
    public interface IResponse<TResult> : IResponse
    {
        TResult Result { get; }
    }
}
