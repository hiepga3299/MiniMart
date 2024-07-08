using AutoMapper;
using MiniMart.Application.DTOs;
using MiniMart.Application.DTOs.Categories;
using MiniMart.Application.DTOs.Products;
using MiniMart.Application.DTOs.ViewModel;
using MiniMart.Domain.Entities;
using MiniMart.Domain.Entities.Enum;
using MiniMart.Infatructure.Abstract;

namespace MiniMart.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageService _image;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IImageService image)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _image = image;
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
            var fileImage = _image.ConverToIFornFile(product.Image);
            var productVM = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Code = product.Code,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Available = product.Available,
                Price = product.Price,
                Image = fileImage,
                IsActive = product.IsActive

            };
            return productVM;
        }

        public async Task<ResponseModel> CreateProduct(ProductViewModel productVM)
        {
            string path = "images/product/";
            string nameImage = $"{productVM.Code}.png";
            var product = _mapper.Map<Product>(productVM);
            product.CreateOn = DateTime.Now;

            if (product.Id == 0)
            {
                product.IsActive = true;
                product.Code = productVM.Code;
            }
            product.Image = path + nameImage;
            await _image.SaveImage(new List<IFormFile> { productVM.Image }, path, nameImage);
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

        public async Task<bool> DeleteProduct(int? id)
        {
            var products = await _unitOfWork.ProductRepository.GetSingleProduct(id);
            if (products != null)
            {
                _unitOfWork.ProductRepository.DeleteProduct(products);
                await _unitOfWork.SaveChage();
                return true;
            }
            return false;
        }

        //Page Shop
        public async Task<ProductForSiteModel> GetListProductForSiteAsync(int categoryId, int pageIndex, int pageSize, string keyword)
        {
            var (products, totalRecords) = await _unitOfWork.ProductRepository.GetAllProductForSite(categoryId, pageIndex, pageSize, keyword);
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            bool isDisable = totalRecords - (pageIndex * pageSize) <= 0 ? true : false;
            return new ProductForSiteModel
            {
                TotalRecord = totalRecords,
                IsDisable = isDisable,
                Products = productDtos
            };
        }

        public async Task<IEnumerable<ProductCartDto>> GetProductByCodeAsync(string[] code)
        {
            var products = await _unitOfWork.ProductRepository.GetListProductByCode(code);
            var result = _mapper.Map<IEnumerable<ProductCartDto>>(products);
            return result;
        }
    }
}
