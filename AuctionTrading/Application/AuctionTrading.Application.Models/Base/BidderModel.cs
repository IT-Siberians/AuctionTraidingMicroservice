using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionTrading.Application.Models.Base
{
    public abstract class PersonModel : IModel<Guid>
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }

    }
}
