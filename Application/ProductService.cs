using AutoMapper;
using MiniMart.Application.DTOs;
using MiniMart.Application.DTOs.ViewModel;
using MiniMart.Domain.Entities;
using MiniMart.Domain.Entities.Enum;
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

        public async Task<ResponseModel> CreateProduct(ProductViewModel productVM)
        {
            var product = _mapper.Map<Product>(productVM);
            product.CreateOn = DateTime.Now;
            if (product.Id == 0)
            {
                product.IsActive = true;
                product.Code = productVM.Code;
            }

            var result = await _unitOfWork.ProductRepository.SaveProduct(product);
            await _unitOfWork.SaveChage();

            var actionType = productVM.Id == 0 ? ActionType.Insert : ActionType.Update;
            var succesMessage = $"{(productVM.Id == 0 ? "Thêm Mới" : "Cập nhập")} Thành Công";
            var errorMessage = $"{(productVM.Id == 0 ? "Thêm Mới" : "Cập nhập")} Thất Bại";

            return new ResponseModel
            {
                Action = actionType,
                Message = (bool)result ? succesMessage : errorMessage,
                Status = (bool)result,
            };
        }

        public async Task<string> GenerateCodeAsync()
        {
            string newCode;
            while (true)
            {
                newCode = Guid.NewGuid().ToString();
                var checkCode = await _unitOfWork.ProductRepository.GetProductByCode(newCode);

                if (checkCode == null)
                {
                    break;
                }
            }
            return newCode;
        }
    }
}
