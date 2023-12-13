using Online_Store.Models;

namespace Online_Store.Services.IService
{
    public interface IOrder
    {
        Task<List<Order>> GetOrdersAsync();

        Task<Order> GetOrderAsync(Guid id);

        Task<string> AddOrder(Order order);

       Task<string>  UpdateOrder(Order order);

        Task<string> DeleteOrder(Order order);
    }
}
