using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniMart.Application.DTOs;
using MiniMart.Application.DTOs.ViewModel;
using MiniMart.Domain.Entities;
using MiniMart.Infatructure.Abstract;

namespace MiniMart.Application
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDataTableModel<CategoryDto>> GetListCategory(RequestDataTableModel requestData)
        {
            var category = await _unitOfWork.CategoryRepository.GetCategoriesAsync();
            var categoryDto = _mapper.Map<IEnumerable<CategoryDto>>(category);
            var totalRecord = categoryDto.Count();
            var pageIndex = categoryDto.Skip(requestData.SkipIndex).Take(requestData.PageSize).ToList();
            return new ResponseDataTableModel<CategoryDto>
            {
                Data = pageIndex,
                Draw = requestData.Draw,
                RecordsFilltered = totalRecord,
                RecordsTotal = totalRecord
            };
        }

        public async Task<CategoryViewModel> GetById(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(id);
            return _mapper.Map<CategoryViewModel>(category);
        }

        public async Task<IEnumerable<SelectListItem>> GetCategoryForDropDownListAsync()
        {
            var category = await _unitOfWork.CategoryRepository.GetCategoriesAsync();
            return category.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
        }

        public async Task CreateCategory(CategoryViewModel categoryViewModel)
        {
            if (categoryViewModel.Id == null)
            {
                var category = new Category
                {
                    Name = categoryViewModel.Name,
                    IsActive = true,
                };
                await _unitOfWork.CategoryRepository.CreateCategoryAsync(category);
            }
            else
            {
                //Update
                var category = await _unitOfWork.CategoryRepository.GetById(categoryViewModel.Id.Value);
                category.Name = categoryViewModel.Name;
                category.IsActive = true;
                _unitOfWork.CategoryRepository.UpdateCategory(category);
            }
        }
    }
}
