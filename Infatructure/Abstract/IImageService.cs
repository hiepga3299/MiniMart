namespace MiniMart.Infatructure.Abstract
{
    public interface IImageService
    {
        Task<bool> SaveImage(List<IFormFile> images, string path, string? defaultName);
    }
}