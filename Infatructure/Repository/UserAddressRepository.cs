using MiniMart.Domain.Entities;
using MiniMart.Infatructure.Abstract;
using MiniMart.Infatructure.DataAccess;

namespace MiniMart.Infatructure.Repository
{
	public class UserAddressRepository : RepositoryBase<Address>, IUserAddressRepository
	{
		public UserAddressRepository(MiniMartDbContext context) : base(context)
		{
		}
		public async Task SaveAsync(Address address)
		{
			if (address.Id == 0)
			{
				await base.Create(address);
			}
			else
			{
				await base.Update(address);
			}
		}
	}
}
