using AuctionTrading.Domain.ValueObject.Base;
using AuctionTrading.Domain.ValueObject.Validation;

namespace AuctionTrading.Domain.ValueObject
{
    /// <summary>
    /// Represents type of the entity's description.
    /// </summary>
    /// <param name="description">The description of the entity.</param>
    public class Description(string description) : ValueObject<string>(new StringValidator(), description)
    {

    }
}
