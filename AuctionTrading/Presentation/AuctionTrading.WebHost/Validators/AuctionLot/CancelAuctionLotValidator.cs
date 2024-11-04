using AuctionTrading.WebHost.Requests.AuctionLot;
using AuctionTrading.WebHost.Validators.Base;
using FluentValidation;

namespace AuctionTrading.WebHost.Validators.AuctionLot
{
    public class CancelAuctionLotValidator : AbstractValidator<CancelAuctionLotRequest>
    {
        public CancelAuctionLotValidator()
        {
            RuleFor(lot => lot.AuctionLotId)
                .SetValidator(new GuidPresentationValidator());

            RuleFor(lot => lot.SellerId)
                .SetValidator(new GuidPresentationValidator());
        }
    }
}
