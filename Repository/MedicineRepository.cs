using Microsoft.EntityFrameworkCore;
using Online_Pharmacy_Management_System.Data;
using Online_Pharmacy_Management_System.Models;

namespace Online_Pharmacy_Management_System.Repository
{
    public class MedicineRepository:IMedicineRepository
    {
        AppDbContext _context;
        public MedicineRepository(AppDbContext context)
        {
            _context=context;
        }
        public async Task AddAsync(Medicine obj)
        {
            await _context.Medicines.AddAsync(obj);
            await SaveAsync();
        }
        public async Task UpdateAsync(Medicine obj)
        {
            _context.Medicines.Update(obj);
            await SaveAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Medicine med = await GetByIdAsync(id);
            if (med != null)
            {
                _context.Medicines.Remove(med);
                await SaveAsync();
            }
        }
        public async Task<List<Medicine>> GetAllAsync()
        {
            return await _context.Medicines.ToListAsync();
        }
        public async Task<Medicine> GetByIdAsync(int id)
        {
            return await _context.Medicines
                .FirstOrDefaultAsync(d => d.MedicineId == id);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        //public async Task<List<Medicine>> SearchAsync(string searchTerm)
        //{
        //    return await _context.Medicines
        //        .Where(m => EF.Functions.Like(m.MedicineName, $"%{searchTerm}%") ||
        //                    EF.Functions.Like(m.Description, $"%{searchTerm}%"))
        //        .ToListAsync();
        //}
    }
}
