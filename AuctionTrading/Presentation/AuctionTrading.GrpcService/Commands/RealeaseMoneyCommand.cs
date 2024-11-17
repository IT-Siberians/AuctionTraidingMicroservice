namespace AuctionTrading.GrpcService.Commands
{
    public record RealeaseMoneyCommand(
    Guid BuyerId,
    Guid LotId,
    decimal Price);
}
