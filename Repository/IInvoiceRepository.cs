using Online_Pharmacy_Management_System.Models;

namespace Online_Pharmacy_Management_System.Repository
{
    public interface IInvoiceRepository
    {
        Task AddAsync(Invoice invoice);
        Task UpdateAsync(Invoice invoice);
        Task DeleteAsync(int id);
        Task<IEnumerable<Invoice>> GetAllAsync();
        Task<Invoice> GetByIdAsync(int id);
    }
}

