using FluentValidation;

namespace AuctionTrading.WebHost.Validators.Base
{
    public class MoneyAmountPresentationValidator : AbstractValidator<decimal>
    {
        public MoneyAmountPresentationValidator()
        {
            RuleFor(x => x)
                .GreaterThan(0m)
                .PrecisionScale(100, 2, false);
        }
    }
}
