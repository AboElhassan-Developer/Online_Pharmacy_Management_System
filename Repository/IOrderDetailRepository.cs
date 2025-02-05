using Online_Pharmacy_Management_System.Models;

namespace Online_Pharmacy_Management_System.Repository
{
    public interface IOrderDetailRepository
    {
        Task AddAsync(OrderDetail orderDetail);
        Task DeleteAsync(int id);
        Task<IEnumerable<OrderDetail>> GetByOrderIdAsync(int orderId); // إضافة هذه السطر

        Task<OrderDetail> GetByIdAsync(int id);
        Task<IEnumerable<OrderDetail>> GetAllAsync();
        Task SaveAsync();
    }
}
