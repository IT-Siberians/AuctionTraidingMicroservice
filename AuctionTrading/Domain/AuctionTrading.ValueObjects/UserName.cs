using AuctionTrading.Domain.ValueObjects.Base;
using AuctionTrading.Domain.ValueObjects.Validators;

namespace AuctionTrading.Domain.ValueObjects
{
    /// <summary>
    /// Represents type of the entity's user name.
    /// </summary>
    /// <param name="name">The user name of the entity.</param>
    public class Username(string name) : ValueObject<string>(new StringValidator(), name)
    {

    }
}
