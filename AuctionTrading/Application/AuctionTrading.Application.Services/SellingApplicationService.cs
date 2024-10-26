using AuctionTrading.Application.Models.Seller;
using AuctionTrading.Application.Services.Abstractions;
using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Repositories.Abstractions;
using AutoMapper;

namespace AuctionTrading.Application.Services
{
    public class SellingApplicationService(
        ISellersRepository sellersRepository,
        ICustomersRepository customersRepository,
        IAuctionLotRepository lotsRepository,
        IRepository<Bid, Guid> bidsRepository,
        IMapper mapper)
        : ISellingApplicationService
    {
        public async Task<bool> CancelAuctionLotAsync(CancelAuctionLotModel information, CancellationToken cancellationToken = default)
        {
            var seller = await sellersRepository.GetByIdAsync(information.SellerId, cancellationToken);
            if (seller is null)
                return false;

            var lot = await lotsRepository.GetByIdAsync(information.AuctionLotId, cancellationToken);
            if (lot is null)
                return false;

            await sellersRepository.UpdateAsync(seller, cancellationToken);
            return seller.CancelLot(lot);
        }
    }
}
