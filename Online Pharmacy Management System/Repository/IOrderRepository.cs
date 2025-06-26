using Online_Pharmacy_Management_System.Models;

namespace Online_Pharmacy_Management_System.Repository
{
    public interface IOrderRepository
    {
        //Task<bool> ExistsBySupplierIdAsync(int supplierId);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(int id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(int id);
        Task SaveAsync();
        //Task<IEnumerable<Order>> SearchAsync(string searchTerm);
    }
}
