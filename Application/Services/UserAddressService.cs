using AutoMapper;
using MiniMart.Application.DTOs;
using MiniMart.Domain.Entities;
using MiniMart.Infatructure.Abstract;

namespace MiniMart.Application.Services
{
	public class UserAddressService : IUserAddressService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public UserAddressService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<int> SaveAsync(UserAddressDto userAddressDto)
		{
			var address = _mapper.Map<Address>(userAddressDto);
			await _unitOfWork.UserAddressRepository.SaveAsync(address);
			await _unitOfWork.SaveChage();
			return address.Id;
		}
	}
}
