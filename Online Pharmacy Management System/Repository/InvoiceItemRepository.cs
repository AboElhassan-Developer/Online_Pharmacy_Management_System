using Microsoft.EntityFrameworkCore;
using Online_Pharmacy_Management_System.Data;
using Online_Pharmacy_Management_System.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Pharmacy_Management_System.Repository
{
    public class InvoiceItemRepository : IInvoiceItemRepository
    {
        private readonly AppDbContext _context;

        public InvoiceItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(InvoiceItem invoiceItem)
        {
            await _context.InvoicesItems.AddAsync(invoiceItem);
            await SaveAsync();
        }

        public async Task UpdateAsync(InvoiceItem invoiceItem)
        {
            _context.InvoicesItems.Update(invoiceItem);
            await SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await GetByIdAsync(id);
            if (item != null)
            {
                _context.InvoicesItems.Remove(item);
                await SaveAsync();
            }
        }

        public async Task<InvoiceItem> GetByIdAsync(int id)
        {
            return await _context.InvoicesItems
                .Include(i => i.Invoice)
                .Include(i => i.Medicine)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<InvoiceItem>> GetAllAsync()
        {
            return await _context.InvoicesItems
                .Include(i => i.Invoice)
                .Include(i => i.Medicine)
                .ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
