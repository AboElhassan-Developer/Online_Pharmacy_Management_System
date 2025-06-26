using Online_Pharmacy_Management_System.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Pharmacy_Management_System.Repository
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
        Task<Category> GetByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task SaveAsync();
    }
}
