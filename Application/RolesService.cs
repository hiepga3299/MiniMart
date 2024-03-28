using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniMart.Application.DTOs;
using MiniMart.Domain.Entities.Enum;
using MiniMart.Infatructure.Abstract;

namespace MiniMart.Application
{
    public class RolesService : IRolesService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RolesService(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SelectListItem>> GetRolesForDropDownList()
        {
            var role = await _roleManager.Roles.ToListAsync();
            return role.Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = x.Name
            });
        }

        public async Task<ResponseDataTableModel<RoleDto>> GetListRolePagination(RequestDataTableModel request)
        {
            var roles = await _roleManager.Roles.Where(x => string.IsNullOrEmpty(request.Keyword) || x.Name == request.Keyword)
                                                .Select(x => new RoleDto { Id = x.Id, Name = x.Name })
                                                .ToListAsync();
            var totalRecord = roles.Count();
            var result = roles.Skip(request.SkipIndex).Take(request.PageSize).ToList();
            return new ResponseDataTableModel<RoleDto>
            {
                Data = result,
                Draw = request.Draw,
                RecordsTotal = totalRecord,
                RecordsFilltered = totalRecord
            };
        }

        public async Task<ResponseModel> CreateRole(RoleDto roleDto)
        {
            var role = new IdentityRole();
            role.Name = roleDto.Name;
            var createSuccess = await _roleManager.CreateAsync(role);

            if (createSuccess.Succeeded)
            {
                return new ResponseModel
                {
                    Action = ActionType.Insert,
                    Message = "Success",
                    Status = true
                };
            }
            return new ResponseModel
            {
                Action = ActionType.Insert,
                Message = "Error",
                Status = false
            };
        }
    }
}
