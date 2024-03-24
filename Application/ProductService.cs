using AutoMapper;
using MiniMart.Application.DTOs;
using MiniMart.Application.DTOs.ViewModel;
using MiniMart.Infatructure.Abstract;

namespace MiniMart.Application
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDataTableModel<ProductDto>> GetListProductPagination(RequestDataTableModel request)
        {
            int totalRecords;
            IEnumerable<ProductDto> products;
            (products, totalRecords) = await _unitOfWork.ProductRepository.GetAllProductPagination<ProductDto>(request.Keyword, request.SkipIndex, request.PageSize);

            return new ResponseDataTableModel<ProductDto>
            {
                Draw = request.Draw,
                RecordsTotal = totalRecords,
                RecordsFilltered = totalRecords,
                Data = products
            };
        }

        public async Task<ProductViewModel> GetProductById(int? id)
        {
            var product = await _unitOfWork.ProductRepository.GetSingleProduct(id);
            var category = (await _unitOfWork.ProductRepository.GetCategoryByProductId<CategoryDto>(id)).First();
            var productVM = _mapper.Map<ProductViewModel>(product);
            productVM.CategoryId = category.Id;
            return productVM;
        }
    }
}
