using AuctionTrading.Domain.ValueObjects.Base;
using AuctionTrading.Domain.ValueObjects.Validators;

namespace AuctionTrading.Domain.ValueObjects
{
    /// <summary>
    /// Represents type of the entity's description.
    /// </summary>
    /// <param name="description">The description of the entity.</param>
    public class Description(string description) : ValueObject<string>(new TitleValidator(), description)
    {

    }
}
