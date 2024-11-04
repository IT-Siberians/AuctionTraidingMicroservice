using AuctionTrading.WebHost.Requests.Customer;
using AuctionTrading.WebHost.Validators.Base;
using FluentValidation;

namespace AuctionTrading.WebHost.Validators.Customer
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerRequest>
    {
        public CreateCustomerValidator()
        {

            RuleFor(customer => customer.Id)
                .SetValidator(new GuidPresentationValidator());

            RuleFor(customer => customer.Username)
                .SetValidator(new UsernamePresentationValidator());
        }
    }
}
