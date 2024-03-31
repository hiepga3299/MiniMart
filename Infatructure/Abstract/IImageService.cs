namespace MiniMart.Infatructure.Abstract
{
    public interface IImageService
    {
        IFormFile ConverToIFornFile(string path);
        Task<bool> SaveImage(List<IFormFile> images, string path, string? defaultName);
    }
}