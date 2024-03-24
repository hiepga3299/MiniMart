using AutoMapper;
using MiniMart.Application.DTOs;
using MiniMart.Application.DTOs.ViewModel;
using MiniMart.Domain.Entities;

namespace MiniMart.Application.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ApplicationUser, AccountDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
