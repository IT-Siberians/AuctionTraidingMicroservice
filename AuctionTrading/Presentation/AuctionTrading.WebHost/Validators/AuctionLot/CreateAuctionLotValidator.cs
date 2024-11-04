using AuctionTrading.WebHost.Requests.AuctionLot;
using AuctionTrading.WebHost.Requests.Seller;
using AuctionTrading.WebHost.Validators.Base;
using FluentValidation;

namespace AuctionTrading.WebHost.Validators
{
    public class CreateAuctionLotValidator : AbstractValidator<CreateAuctionLotRequest>
    {
        public CreateAuctionLotValidator()
        {
            RuleFor(lot => lot.Id)
                .SetValidator(new GuidPresentationValidator());

            RuleFor(lot => lot.Title)
                .SetValidator(new TitlePresentationValidator());

            RuleFor(lot => lot.Description)
                .SetValidator(new DescriptionPresentationValidator());

            RuleFor(lot => lot.StartPrice)
                .SetValidator(new MoneyAmountPresentationValidator());

            RuleFor(lot => lot.BidIncrement)
                .SetValidator(new MoneyAmountPresentationValidator());

            RuleFor(lot => lot.RepurchasePrice)
                .SetValidator(new RepurchasePricePresentationValidator())
                .GreaterThan(lot => lot.StartPrice);

            RuleFor(lot => lot.StartDate.ToUniversalTime())
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.UtcNow);

            RuleFor(lot => lot.EndDate.ToUniversalTime())
                .NotEmpty()
                .GreaterThan(lot => lot.StartDate)
                .GreaterThan(DateTime.UtcNow);

            RuleFor(lot => lot.SellerId)
                .SetValidator(new GuidPresentationValidator());
        }
    }
}
