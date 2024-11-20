using AuctionTrading.WebHost.Requests.Bid;
using AuctionTrading.WebHost.Validators.Base;
using FluentValidation;

namespace AuctionTrading.WebHost.Validators.Bid
{
    public class CreateBidValidator : AbstractValidator<CreateBidRequest>
    {
        public CreateBidValidator()
        {
            RuleFor(bid => bid.AuctionLotId)
                .SetValidator(new GuidPresentationValidator());

            RuleFor(bid => bid.CustomerId)
                .SetValidator(new GuidPresentationValidator());

            RuleFor(bid => bid.Amount)
                .SetValidator(new MoneyAmountPresentationValidator());
        }
    }
}
