using AutoMapper;
using Domain.UserAggregate;
using Infrastructure.DTO.Request;
using Infrastructure.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mapper
{
    internal class MainMapper : Profile
    {
        public MainMapper() {
            CreateMap<CreateUserRequestDto, User>()
                .ReverseMap();

            CreateMap<User, CreateUserResponseDto>()
                .ReverseMap();

            CreateMap<User, UserResponseDto>()
                .ForMember(x => x.isActive, cd => cd.MapFrom(map => map.RevokedOn == null))
                .ReverseMap();
        }
    }
}
