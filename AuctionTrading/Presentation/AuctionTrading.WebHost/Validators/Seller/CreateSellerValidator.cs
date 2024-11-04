using AuctionTrading.WebHost.Requests.Seller;
using AuctionTrading.WebHost.Validators.Base;
using FluentValidation;

namespace AuctionTrading.WebHost.Validators.Seller
{
    public class CreateSellerValidator : AbstractValidator<CreateSellerRequest>
    {
        public CreateSellerValidator()
        {

            RuleFor(seller => seller.Id)
                .SetValidator(new GuidPresentationValidator());

            RuleFor(seller => seller.Username)
                .SetValidator(new UsernamePresentationValidator());
        }
    }
}
