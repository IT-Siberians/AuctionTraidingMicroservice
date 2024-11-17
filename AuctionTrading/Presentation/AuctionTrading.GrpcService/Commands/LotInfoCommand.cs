namespace AuctionTrading.GrpcService.Commands
{
    public record LotInfoCommand(
            Guid Id,
            string Title,
            string Description);
}
