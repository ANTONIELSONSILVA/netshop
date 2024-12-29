using netShop.Web.Models;

namespace netShop.Web.Services.Contracts;

public interface IProductServices
{
    Task<IEnumerable<ProductViewModel>> GetAllProducts();
    Task<ProductViewModel> FindProductById(int id);
    Task<ProductViewModel> CreateProduct(ProductViewModel productVM);
    Task<ProductViewModel> UpdateProduct(ProductViewModel productVM);
    Task<bool> DeleteProductById(int id);

}
