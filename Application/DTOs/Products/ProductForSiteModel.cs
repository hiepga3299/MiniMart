namespace MiniMart.Application.DTOs.Products
{
    public class ProductForSiteModel
    {
        public int TotalRecord { get; set; }
        public bool IsDisable { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
