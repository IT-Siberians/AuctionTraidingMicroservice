namespace AuctionTrading.GrpcService.Commands
{
    public record ReserveMoneyCommand(
    Guid BuyerId,
    decimal Price,
    LotInfoModel Lot);
}
