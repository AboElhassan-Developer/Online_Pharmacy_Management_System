using Online_Pharmacy_Management_System.Models;
using Online_Pharmacy_Management_System.ViewModel;

namespace Online_Pharmacy_Management_System.Repository
{
    public interface IInvoiceItemRepository
    {
        Task AddAsync(InvoiceItem invoiceItem);
        Task UpdateAsync(InvoiceItem invoiceItem);
        Task DeleteAsync(int id);
        Task<IEnumerable<InvoiceItem>> GetAllAsync();
        Task<InvoiceItem> GetByIdAsync(int id);
    }
}
