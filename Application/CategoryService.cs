using AutoMapper;
using MiniMart.Application.DTOs;
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
    }
}
