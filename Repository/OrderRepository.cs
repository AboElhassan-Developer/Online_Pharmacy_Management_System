using Microsoft.EntityFrameworkCore;
using Online_Pharmacy_Management_System.Data;
using Online_Pharmacy_Management_System.Models;

namespace Online_Pharmacy_Management_System.Repository
{
    public class OrderRepository: IOrderRepository
    {
        AppDbContext _context;
        public OrderRepository(AppDbContext context) 
        {
            _context = context;
        }
       
        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await GetByIdAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await SaveAsync();
            }
        }
        public async Task UpdateAsync(Order order)
        {
            var existingOrder = await GetByIdAsync(order.OrderId);
            if (existingOrder != null)
            {
                _context.Entry(existingOrder).CurrentValues.SetValues(order);
                await SaveAsync();
            }
            //_context.Orders.Update(order);
            //await SaveAsync();
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
               .Include(o => o.OrderDetails)
                   .ThenInclude(od => od.Medicine)
               .Include(o => o.Patient)
               .Include(o => o.Supplier)
               //.AsNoTracking()
               .ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders
                 .Include(o => o.OrderDetails)
             .ThenInclude(od => od.Medicine)  
         .Include(o => o.Patient)           
         .Include(o => o.Supplier)          
         .FirstOrDefaultAsync(o => o.OrderId == id);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        //public async Task<IEnumerable<Order>> SearchAsync(string searchTerm)
        //{
        //    return await _context.Orders
        //        .Where(o => o.Status.Contains(searchTerm) ||
        //                    o.Patient.Name.Contains(searchTerm) ||
        //                    o.Supplier.Name.Contains(searchTerm))
        //        .ToListAsync();
        //}
    }
}
