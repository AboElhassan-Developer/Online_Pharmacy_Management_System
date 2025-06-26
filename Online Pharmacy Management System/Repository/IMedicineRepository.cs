using Online_Pharmacy_Management_System.Models;

namespace Online_Pharmacy_Management_System.Repository
{
    public interface IMedicineRepository
    {
        Task AddAsync(Medicine obj);

        Task UpdateAsync(Medicine obj);

        Task DeleteAsync(int id);

        Task<List<Medicine>> GetAllAsync();

        Task<Medicine> GetByIdAsync(int id);

        Task SaveAsync();

        //Task<List<Medicine>> SearchAsync(string searchTerm);
    }
}
