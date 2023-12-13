using Online_Store.Models;
using Online_Store.Services.IService;

namespace Online_Store.Services
{
    public class OrderService : IOrder
    {
        
        public Task<string> AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
