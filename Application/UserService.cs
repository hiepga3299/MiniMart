using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniMart.Application.DTOs;
using MiniMart.Domain.Entities;
using MiniMart.Domain.Entities.Enum;
using MiniMart.Infatructure.Abstract;

namespace MiniMart.Application
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<ResponseModel> CheckLogin(string username, string password, bool hasRemember)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Username or Password is invalid"
                };
            }
            var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: hasRemember, lockoutOnFailure: true);
            if (result.IsLockedOut)
            {
                var reminingLockout = user.LockoutEnd - DateTimeOffset.UtcNow;
                return new ResponseModel
                {
                    Status = false,
                    Message = $"Account is lockout. Please try again in {Math.Round(reminingLockout.Value.TotalMinutes)} minutes"
                };
            }
            if (!result.Succeeded)
            {
                return new ResponseModel
                {
                    Message = "Username or Password is invalid"
                };
            }
            if (user.AccessFailedCount > 0)
            {
                await _userManager.ResetAccessFailedCountAsync(user);
            }
            return new ResponseModel
            {
                Status = true
            };
        }

        public async Task<ResponseListAccountModel<UserDto>> GetListUser(RequestModel requestModel)
        {
            var user = await _userManager.Users.Where(x => string.IsNullOrEmpty(requestModel.Keyword)
                                                                || (x.UserName.Contains(requestModel.Keyword)
                                                                || x.Fullname.Contains(requestModel.Keyword)
                                                                || x.Email.Contains(requestModel.Keyword))
                                                                )
                              .Select(x => new UserDto
                              {
                                  Id = x.Id,
                                  Username = x.UserName,
                                  Fullname = x.Fullname,
                                  Email = x.Email,
                                  Phone = x.PhoneNumber,
                                  IsActive = x.IsActive ? "Yes" : "No"
                              }).ToListAsync();
            var totalRecord = user.Count();
            var result = user.Skip(requestModel.SkipIndex).Take(requestModel.PageSize).ToList();
            return new ResponseListAccountModel<UserDto>
            {
                Data = result,
                Draw = requestModel.Draw,
                RecordsTotal = totalRecord,
                RecordsFilltered = totalRecord
            };
        }

        public async Task<AccountDto> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var role = (await _userManager.GetRolesAsync(user)).First();
            var userDto = _mapper.Map<AccountDto>(user);
            userDto.Role = role;
            return userDto;
        }

        public async Task<ResponseModel> SaveAccount(AccountDto accountDto)
        {
            var errors = string.Empty;
            IdentityResult identityResult;
            if (accountDto.Id == null)
            {
                //Insert
                var applicationUser = new ApplicationUser
                {
                    Fullname = accountDto.Fullname,
                    UserName = accountDto.Username,
                    Email = accountDto.Email,
                };
                identityResult = await _userManager.CreateAsync(applicationUser, accountDto.Password);
                if (identityResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(applicationUser, accountDto.Role);
                    return new ResponseModel
                    {
                        Action = ActionType.Insert,
                        Message = "Succsess",
                        Status = true
                    };
                }
            }
            else
            {
                //Update
                var user = await _userManager.FindByIdAsync(accountDto.Id);
                user.Fullname = accountDto.Fullname;
                user.Email = accountDto.Email;
                identityResult = await _userManager.UpdateAsync(user);

                if (identityResult.Succeeded)
                {
                    var hasRole = await _userManager.IsInRoleAsync(user, accountDto.Role);
                    if (!hasRole)
                    {
                        var oldRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                        var removeRole = await _userManager.RemoveFromRoleAsync(user, oldRole);
                        if (removeRole.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, accountDto.Role);
                        }
                    }
                    return new ResponseModel
                    {
                        Action = ActionType.Update,
                        Message = "Update succsesful.",
                        Status = true
                    };
                }
            }
            errors = string.Join("</br>", identityResult.Errors.Select(x => x.Description));

            return new ResponseModel
            {
                Action = ActionType.Insert,
                Message = $"{(string.IsNullOrEmpty(accountDto.Id) ? "Insert" : "Update")} failed {errors}",
                Status = true
            };
        }
    }
}
