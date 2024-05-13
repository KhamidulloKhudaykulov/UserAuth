using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAuth.Domain.Entities;
using UserAuth.Model.Users;

namespace UserAuth.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserCreateModel>().ReverseMap();
        CreateMap<User, UserUpdateModel>().ReverseMap();
        CreateMap<User, UserViewModel>().ReverseMap();
    }
}
