using AuthServer.Core.DataTransferObjects;
using AuthServer.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Service
{
    class DTOMapper : Profile
    {
        public DTOMapper()
        {
            CreateMap<ProductDTOs, Product>().ReverseMap();

            CreateMap<UserAppDTOs, UserApp>().ReverseMap();
        }
    }
}
