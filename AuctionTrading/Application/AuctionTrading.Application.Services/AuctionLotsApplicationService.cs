
using AuctionTrading.Application.Models.AuctionLot;
using AuctionTrading.Application.Models.Seller;
using AuctionTrading.Application.Services.Abstractions;
using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Repositories.Abstractions;
using AuctionTrading.Domain.ValueObjects;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionTrading.Application.Services
{
    public class AuctionLotsApplicationService(IAuctionLotRepository lotRepository, ISellersRepository sellersRepository, IMapper mapper) : IAuctionLotsApplicationService
    {
        public async Task<IEnumerable<AuctionLotModel>> GetAuctionLotsAsync()
            => (await lotRepository.GetAllAsync()).Select(mapper.Map<AuctionLotModel>);

        public async Task<AuctionLotModel?> GetAuctionLotByIdAsync(Guid id)
        {
            var lot = await lotRepository.GetByIdAsync(id);
            return lot is null ? null : mapper.Map<AuctionLotModel>(lot);
        }

        public async Task<bool> CreateAuctionLotAsync(CreateAuctionLotModel auctionLotInformation)
        {
            var seller = await sellersRepository.GetByIdAsync(auctionLotInformation.SellerId);
            if (seller is null)
                return false;
            AuctionLot lot = new(
                auctionLotInformation.Id,
                new(auctionLotInformation.Title),
                new(auctionLotInformation.Description),
                new(auctionLotInformation.StartPrice),
                new(auctionLotInformation.BidIncrement),
                auctionLotInformation.RepurchasePrice is null ? null : new(auctionLotInformation.RepurchasePrice.Value),
                auctionLotInformation.StartDate,
                auctionLotInformation.EndDate,
                seller);
            await sellersRepository.UpdateAsync(seller); // не уверена, что эта строчка нужна!
            return await lotRepository.AddAsync(lot);
        }

        public async Task<bool> UpdateAuctionLotAsync(AuctionLotModel auctionLot)
        {
            var entity = await lotRepository.GetByIdAsync(auctionLot.Id);
            if (entity is null)
                return false;
            entity = mapper.Map<AuctionLot>(auctionLot);
            return await lotRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAuctionLotAsync(Guid id)
        {
            var lot = await lotRepository.GetByIdAsync(id);
            return lot is null ? false : await lotRepository.DeleteAsync(lot);
        }
    }
}
