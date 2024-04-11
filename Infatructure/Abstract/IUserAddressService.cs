using MiniMart.Application.DTOs;

namespace MiniMart.Infatructure.Abstract
{
	public interface IUserAddressService
	{
		Task<int> SaveAsync(UserAddressDto userAddressDto);
	}
}