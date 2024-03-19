
namespace MiniMart.Application
{
    public interface IImageService
    {
        Task<bool> SaveImage(List<IFormFile> images, string path, string? defaultName);
    }
}