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

        public async Task<List<Products>> GetProductsAsync(int page,int pageSize)
        {
                List<Products> products = _context.Products.ToList();
                int skip = (page - 1) * pageSize;

            // Apply pagination using LINQ Skip and Take methods
             var paginatedProducts = products.Skip(skip).Take(pageSize).ToList();
            return paginatedProducts;
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

        public async Task<List<Products>> GetByFilterAsync(string productName,int productPrice)
        {
            //Check based on the name and price
            IQueryable<Products> query = _context.Products;
            if (!string.IsNullOrEmpty(productName))
            {
                query = query.Where(p => p.Name.Contains(productName));
            }
            if (productPrice>0)
            {
                query = query.Where(p => p.price<= productPrice);
            }
           var result =   query.ToList();
            return result;
        }
    }
}
