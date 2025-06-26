using Microsoft.EntityFrameworkCore;
using Online_Pharmacy_Management_System.Data;
using Online_Pharmacy_Management_System.Models;

namespace Online_Pharmacy_Management_System.Repository
{
    public class PatientRepository:IPatientRepository
    {
        private readonly AppDbContext _context;
        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var patient = await GetByIdAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await SaveAsync();
            }
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.PatientId == id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Patient>> SearchAsync(string searchTerm)
        {
            return await _context.Patients
                .Where(p => EF.Functions.Like(p.Name, $"%{searchTerm}%") ||
                            EF.Functions.Like(p.Email, $"%{searchTerm}%"))
                .ToListAsync();
        }

        public async Task UpdateAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await SaveAsync();
        }
    }
}
