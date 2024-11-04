using FluentValidation;

namespace AuctionTrading.WebHost.Validators.Base
{
    public class RepurchasePricePresentationValidator : AbstractValidator<decimal?>
    {
        public RepurchasePricePresentationValidator()
        {
            RuleFor(x => x)
                .GreaterThan(0m)
                .PrecisionScale(100, 2, false);
        }
    }
}