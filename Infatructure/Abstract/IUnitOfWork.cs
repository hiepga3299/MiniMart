namespace MiniMart.Infatructure.Abstract
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }

        Task SaveChage();
    }
}