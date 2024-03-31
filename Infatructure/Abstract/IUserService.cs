using MiniMart.Application.DTOs;
using MiniMart.Application.DTOs.Accounts;

namespace MiniMart.Infatructure.Abstract
{
    public interface IUserService
    {
        Task<ResponseModel> CheckLogin(string username, string password, bool hasRemember);
        Task<bool> Disable(string id);
        Task<ResponseDataTableModel<UserDto>> GetListUser(RequestDataTableModel requestModel);
        Task<AccountDto> GetUserById(string id);
        Task<ResponseModel> SaveAccount(AccountDto accountDto);
    }
}