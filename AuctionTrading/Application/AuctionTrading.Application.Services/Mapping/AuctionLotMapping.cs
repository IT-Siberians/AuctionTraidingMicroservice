using AuctionTrading.Application.Models.AuctionLot;
using AuctionTrading.Domain.Entities;
using AutoMapper;

namespace GradeBookMicroservice.Application.Services.Mapping
{
    public class AuctionLotMapping : Profile
    {
        public AuctionLotMapping()
        {
            CreateMap<AuctionLot, AuctionLotModel>().ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value));
        }

    }
}