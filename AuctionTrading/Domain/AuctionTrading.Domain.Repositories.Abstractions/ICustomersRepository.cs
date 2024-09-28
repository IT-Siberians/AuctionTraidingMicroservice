using AuctionTrading.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionTrading.Domain.Repositories.Abstractions
{
    public interface ICustomersRepository : IRepository<Customer, Guid>
    {
        Task<Customer?> GetCustomerByUsernameAsync(string username);

    }
}
