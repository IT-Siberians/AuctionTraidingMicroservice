using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionTrading.Domain.Base
{
    /// <summary>
    /// Represents an entity in the system.
    /// </summary>
    /// <typeparam name="TId">The type of the entity's ID.</typeparam>
    /// <param name="id">The ID of the entity.</param>
    /// <remarks>
    /// Initializes a new instance of the <see cref="Entity{TId}"/> class.
    /// </remarks>
    /// <param name="id">The ID of the entity.</param>
    public abstract class Entity<TId>(TId id) where TId : struct
    {
        /// <summary>
        /// Gets the ID of the entity.
        /// </summary>
        public TId Id { get; }
        /// <summary>
        /// Protected constructor for entity framework if needed.
        /// </summary>
        protected Entity() : this(default!)
        {

        }
    }
}
