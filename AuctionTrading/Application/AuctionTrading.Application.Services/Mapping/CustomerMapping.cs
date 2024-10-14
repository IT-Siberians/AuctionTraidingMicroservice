using AuctionTrading.Application.Models.Customer;
using AuctionTrading.Domain.Entities;
using AutoMapper;

namespace GradeBookMicroservice.Application.Services.Mapping
{
    public class CustomerMapping : Profile
    {
        public CustomerMapping()
        {
            CreateMap<Customer, CustomerModel>().ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username.Value));
        }

    }
}