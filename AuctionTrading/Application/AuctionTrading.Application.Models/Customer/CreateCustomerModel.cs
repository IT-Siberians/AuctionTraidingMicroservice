using AuctionTrading.Application.Models.Base;

namespace AuctionTrading.Application.Models.Customer
{
    public record class CreateCustomerModel(Guid Id, string Username)
        : BidderCreateModel(Id, Username);

}
