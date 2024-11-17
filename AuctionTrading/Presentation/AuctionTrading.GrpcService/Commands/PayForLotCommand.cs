namespace AuctionTrading.GrpcService.Commands
{
    public record PayForLotCommand(
    Guid BuyerId,
    Guid SellerId,
    Guid LotId,
    decimal HammerPrice);
}
