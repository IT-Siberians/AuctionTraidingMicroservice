using AuctionTrading.Domain.ValueObjects.Validators;
using FluentValidation;

namespace AuctionTrading.WebHost.Validators.Base
{
    public class TitlePresentationValidator:AbstractValidator<string>
    {
        public TitlePresentationValidator()
        {
            RuleFor(request => request)
                .NotEmpty()
                .NotNull()
                .MinimumLength(TitleValidator.MIN_LENGTH)
                .MaximumLength(TitleValidator.MAX_LENGTH);
        }
    }
}
