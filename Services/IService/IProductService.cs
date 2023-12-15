using Online_Store.Models;

namespace Online_Store.Services.IService
{
    public interface IProductService
    {
        Task<List<Products>> GetProductsAsync(int page,int pageSize);

        Task<Products> GetProductAsync(Guid id);

        Task<string> AddProduct(Products product);

        Task<string> UpdateProduct(Products product);

        Task<string> DeleteProduct(Products product);

        Task<List<Products>> GetByFilterAsync(string productName, int productPrice);
    }
}
