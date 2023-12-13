using Microsoft.EntityFrameworkCore;
using Online_Store.Data;
using Online_Store.Models;
using Online_Store.Services.IService;

namespace Online_Store.Services
{
    public class OrderService : IOrder
    {
        private readonly OnlineStoreDbContext _context;
        public OrderService(OnlineStoreDbContext context) {
            _context = context;
        }
        
        public async Task<string> AddOrder(Order order)
        {
            try
            {
               _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return "successfully created order";
            }
            catch(Exception e)
            {
                Console.WriteLine($"{e.InnerException}");
                return $"{e.InnerException}";
            }
          
        }

        public async Task<string> DeleteOrder(Order order)
        {
            try
            {
                _context.Orders.Remove(order);
                _context.SaveChangesAsync();
                return "removed successfully";

            }catch(Exception e)
            {
                return $"{e.InnerException}";
            }
        }

        public async Task<Order> GetOrderAsync(Guid id)
        {
          
           Order order = await  _context.Orders.FindAsync(id);
                if(order != null)
                {
                    return order;
                }
            return order;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            List<Order> orders = _context.Orders.Include(x=>x.Products).ToList();
            return orders;
        }

        public async Task<string> UpdateOrder(Order order)
        {
            try
            {
                _context.Orders.Update(order);
               _context.SaveChanges();
                return "Success";
            }
            catch (Exception e)
            {
                return $"{e.InnerException}";
            }
        }
    }
}
