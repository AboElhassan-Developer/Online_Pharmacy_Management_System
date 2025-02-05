using Microsoft.EntityFrameworkCore;
using Online_Pharmacy_Management_System.Data;
using Online_Pharmacy_Management_System.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Pharmacy_Management_System.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Category category)
        {
            bool exists = await _context.Categories.AnyAsync(c => c.Name == category.Name);
            if (exists)
            {
                throw new InvalidOperationException("Category name already exists.");
            }
            await _context.Categories.AddAsync(category);
            await SaveAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            bool exists = await _context.Categories.AnyAsync(c => c.Name == category.Name && c.CategoryId != category.CategoryId);
            if (exists)
            {
                throw new InvalidOperationException("Category name already exists.");
            }
            _context.Categories.Update(category);
            await SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await GetByIdAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await SaveAsync();
            }
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.Medicines) // إضافة الأدوية المرتبطة بالفئة
                .FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .Include(c => c.Medicines) // إضافة الأدوية المرتبطة بالفئة
                .ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
