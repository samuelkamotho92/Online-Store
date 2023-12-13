using Online_Store.Data;
using Online_Store.Dtos;
using Online_Store.Models;
using Online_Store.Services.IService;

namespace Online_Store.Services
{
    public class ProductService : IProductService
    {
        private readonly OnlineStoreDbContext _context;


        public ProductService(OnlineStoreDbContext context) { 
        _context = context;
        }
        public async Task<string> AddProduct(Products product)
        {
            try
            {
                await  _context.Products.AddAsync(product);
                _context.SaveChanges();
                return "success";
            }catch(Exception ex)
            {
                return $"Something went wrong {ex.InnerException}";
            }
        }

        public async Task<string> DeleteProduct(Products product)
        {
            try
            {
                _context.Products.Remove(product);
               await  _context.SaveChangesAsync();
                return "Removed product successfuly";
            }catch(Exception ex)
            {
                return $"Something went wrong {ex.InnerException}";
            }
          
        }

        public async Task<Products> GetProductAsync(Guid id)
        {
         Products product = await _context.Products.FindAsync(id);
           if(product != null)
            {
                return product;
            }
            return product;
         
        }

        public async Task<List<Products>> GetProductsAsync()
        {
                List<Products> products = _context.Products.ToList();
                return products;
        }

        public async Task<string> UpdateProduct(Products prod)
        {
            try
            {
                _context.Products.Update(prod);
              await  _context.SaveChangesAsync();

                return "updated successfully";
            }
            catch(Exception e)
            {
                return ($"something went very wrong {e.InnerException}");
            }
    
        }
    }
}
