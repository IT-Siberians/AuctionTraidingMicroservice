using AuctionTrading.Domain.ValueObjects.Validators;
using FluentValidation;

namespace AuctionTrading.WebHost.Validators.Base
{
    public class UsernamePresentationValidator : AbstractValidator<string>
    {
        public UsernamePresentationValidator()
        {
            RuleFor(request => request)
                .NotEmpty()
                .NotNull()
                .MinimumLength(UsernameValidator.MIN_LENGTH)
                .MaximumLength(UsernameValidator.MAX_LENGTH);
        }
    }
}
