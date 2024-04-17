using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MiniMart.Application.DTOs;
using MiniMart.Application.DTOs.Accounts;
using MiniMart.Application.DTOs.Cart;
using MiniMart.Application.DTOs.Categories;
using MiniMart.Application.DTOs.Order;
using MiniMart.Application.DTOs.Products;
using MiniMart.Application.DTOs.Report;
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
            CreateMap<Product, ProductCartDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap().ForMember(destination => destination.CategoryId, option => option.MapFrom(source => source.CategoryId));
            CreateMap<Address, UserAddressDto>().ReverseMap().ForMember(destination => destination.AddressName, option => option.MapFrom(source => source.Address)).ForMember(destination => destination.Phone, option => option.MapFrom(source => source.PhoneNumber));
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<CartRequestDto, Cart>().ReverseMap().ForMember(destination => destination.Status, option => option.MapFrom(source => Convert.ToInt16(source.Status)));
            CreateMap<Order, OrderRequestDto>().ReverseMap();
            CreateMap<OrderDetail, DetailOrderDto>().ReverseMap();
            CreateMap<Address, OrderAddressDto>()
                .ForMember(destination => destination.Address, option => option.MapFrom(source => source.AddressName))
                .ForMember(destination => destination.Name, option => option.MapFrom(source => source.Fullname))
                .ReverseMap();

        }
    }
}
