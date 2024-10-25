using AuctionTrading.Application.Models.Bid;
using AuctionTrading.Application.Models.Seller;
using AuctionTrading.Application.Services.Abstractions;
using AuctionTrading.Common.Enums;
using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Repositories.Abstractions;
using AutoMapper;
using Microsoft.VisualBasic;

namespace AuctionTrading.Application.Services
{
    public class BidderApplicationService(
        ISellersRepository sellersRepository,
        ICustomersRepository customersRepository,
        IAuctionLotRepository lotsRepository,
        IRepository<Bid, Guid> bidsRepository,
        IMapper mapper)
        : IBidderApplicationService
    {
        public async Task<BidStatus> MakeBidAsync(CreateBidModel bidInformation)
        {
            var customer = await customersRepository.GetByIdAsync(bidInformation.CustomerId);
            if (customer is null)
                return BidStatus.FaultedCustomerNotFound;

            var lot = await lotsRepository.GetByIdAsync(bidInformation.AuctionLotId);
            if (lot is null)
                return BidStatus.FaultedLotNotFound;

            var bidStatus = customer.TryMakeBid(lot, new(bidInformation.Amount));

            if (bidStatus == BidStatus.Success)
            {
                // не уверена, что эта строчка нужна!
                Task.WaitAll(customersRepository.UpdateAsync(customer), lotsRepository.UpdateAsync(lot));
            }

            return bidStatus; 
        }
    }
}
