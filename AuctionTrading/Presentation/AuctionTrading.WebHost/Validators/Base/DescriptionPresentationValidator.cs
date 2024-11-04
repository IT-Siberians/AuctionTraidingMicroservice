using AuctionTrading.Domain.ValueObjects.Validators;
using FluentValidation;

namespace AuctionTrading.WebHost.Validators.Base
{
    public class DescriptionPresentationValidator : AbstractValidator<string>
    {
        public DescriptionPresentationValidator()
        {
            RuleFor(request => request)
                .NotEmpty()
                .NotNull();
        }
    }
}
