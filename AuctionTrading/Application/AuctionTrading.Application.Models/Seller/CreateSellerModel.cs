using AuctionTrading.Application.Models.Base;

namespace AuctionTrading.Application.Models.Seller
{
    public record class CreateSellerModel(Guid Id, string Username)
        : BidderCreateModel(Id, Username);

}
