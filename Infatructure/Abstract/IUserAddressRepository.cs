using MiniMart.Domain.Entities;

namespace MiniMart.Infatructure.Abstract
{
	public interface IUserAddressRepository
	{
		Task SaveAsync(Address address);
	}
}