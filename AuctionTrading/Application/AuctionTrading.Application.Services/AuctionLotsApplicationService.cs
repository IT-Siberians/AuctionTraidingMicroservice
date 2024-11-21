using AuctionTrading.Application.Models.AuctionLot;
using AuctionTrading.Application.Services.Abstractions;
using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Repositories.Abstractions;
using AutoMapper;

namespace AuctionTrading.Application.Services
{
    public class AuctionLotsApplicationService(IAuctionLotRepository lotRepository, ISellersRepository sellersRepository, IMapper mapper) : IAuctionLotsApplicationService
    {
        public async Task<IEnumerable<AuctionLotModel>> GetAuctionLotsAsync(CancellationToken cancellationToken = default)
            => (await lotRepository.GetAllAsync(cancellationToken, true))
                .Where(l => l.IsActive)
                .Select(mapper.Map<AuctionLotModel>);

        public async Task<IEnumerable<AuctionLotModel>> GetAuctionLotsByEndDateAsync(DateTime endDateUtc, CancellationToken cancellationToken = default)
            => (await lotRepository.GetAllByEndDateAsync(endDateUtc, cancellationToken, true)).Select(mapper.Map<AuctionLotModel>);

        public async Task<AuctionLotModel?> GetAuctionLotByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var lot = await lotRepository.GetByIdAsync(id, cancellationToken);
            return lot is null ? null : mapper.Map<AuctionLotModel>(lot);
        }

        public async Task<AuctionLotModel?> CreateAuctionLotAsync(CreateAuctionLotModel auctionLotInformation, CancellationToken cancellationToken = default)
        {
            var seller = await sellersRepository.GetByIdAsync(auctionLotInformation.SellerId, cancellationToken);
            if (seller is null)
                return null;

            if (await lotRepository.GetByIdAsync(auctionLotInformation.Id, cancellationToken) is not null)
                return null;

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
            var cteatedLot = await lotRepository.AddAsync(lot, cancellationToken);
            return cteatedLot is null ? null : mapper.Map<AuctionLotModel>(cteatedLot);
        }

        public async Task<bool> UpdateAuctionLotAsync(AuctionLotModel auctionLot, CancellationToken cancellationToken = default)
        {
            var entity = await lotRepository.GetByIdAsync(auctionLot.Id, cancellationToken);
            if (entity is null)
                return false;

            entity = mapper.Map<AuctionLot>(auctionLot);
            return await lotRepository.UpdateAsync(entity, cancellationToken);
        }

        public async Task<bool> FinalizeAuctionLotAsync(CancellationToken cancellationToken = default)
        {
            var entities = await lotRepository.GetAllAsync(cancellationToken, true);
            if (entities is null)
                return false;

            foreach (var entity in entities)
            {
                entity.SetComplete();
                await lotRepository.UpdateAsync(entity, cancellationToken);
            }
            return true;
        }

        public async Task<bool> DeleteAuctionLotAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var lot = await lotRepository.GetByIdAsync(id, cancellationToken);
            return lot is null ? false : await lotRepository.DeleteAsync(lot, cancellationToken);
        }
    }
}
