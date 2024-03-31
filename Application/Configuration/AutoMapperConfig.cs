using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MiniMart.Application.DTOs.Accounts;
using MiniMart.Application.DTOs.Categories;
using MiniMart.Application.DTOs.Products;
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
            CreateMap<IdentityRole, RoleDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap().ForMember(destination => destination.CategoryId, option => option.MapFrom(source => source.CategoryId));
        }
    }
}
