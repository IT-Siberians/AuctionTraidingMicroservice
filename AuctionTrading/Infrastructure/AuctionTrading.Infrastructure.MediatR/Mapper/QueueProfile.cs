using AuctionTrading.Application.Models.Customer;
using AuctionTrading.Application.Models.Seller;
using AutoMapper;
using Otus.QueueDto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionTrading.Infrastructure.MediatR.Mapper
{
    public class QueueProfile:Profile
    {
        public QueueProfile()
        {
            CreateMap<CreateSellerModel, CreateUserEvent>()
        .ReverseMap();
            CreateMap<CreateCustomerModel, CreateUserEvent>()
.ReverseMap();
        }
    }
}
