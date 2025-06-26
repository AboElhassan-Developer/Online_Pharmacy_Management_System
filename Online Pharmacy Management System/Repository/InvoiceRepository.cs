using Microsoft.EntityFrameworkCore;
using Online_Pharmacy_Management_System.Data;
using Online_Pharmacy_Management_System.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Online_Pharmacy_Management_System.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context;
        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Invoice invoice)
        {
            invoice.TotalAmount = invoice.InvoicesItems.Sum(i => i.Subtotal);
            await _context.Invoices.AddAsync(invoice);
            await SaveAsync();
        }

        public async Task UpdateAsync(Invoice invoice)
        {
            invoice.TotalAmount = invoice.InvoicesItems.Sum(i => i.Subtotal);
            _context.Invoices.Update(invoice);
            await SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var invoice = await GetByIdAsync(id);
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
                await SaveAsync();
            }
        }

        public async Task<Invoice> GetByIdAsync(int id)
        {
            return await _context.Invoices
                .Include(i => i.Patient)
                .Include(i => i.InvoicesItems)
                .ThenInclude(ii => ii.Medicine)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await _context.Invoices
                .Include(i => i.Patient)
                .Include(i => i.InvoicesItems)
                .ThenInclude(ii => ii.Medicine)
                .ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}