using AuctionTrading.Application.Models.AuctionLot;
using AuctionTrading.Application.Models.Bid;
using AuctionTrading.Domain.Entities;
using AutoMapper;

namespace GradeBookMicroservice.Application.Services.Mapping
{
    public class BidMapping : Profile
    {
        public BidMapping()
        {
            CreateMap<Bid, BidModel>().ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount.Value));
        }

    }
}