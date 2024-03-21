namespace MiniMart.Infatructure.Abstract
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        Task SaveChage();
    }
}