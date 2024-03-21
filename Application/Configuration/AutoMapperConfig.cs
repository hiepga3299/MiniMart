using AutoMapper;
using MiniMart.Application.DTOs;
using MiniMart.Domain.Entities;

namespace MiniMart.Application.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ApplicationUser, AccountDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
