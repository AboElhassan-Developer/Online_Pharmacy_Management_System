using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Pharmacy_Management_System.Data;
using Online_Pharmacy_Management_System.Models;
using Online_Pharmacy_Management_System.Repository;
using Online_Pharmacy_Management_System.ViewModel;

namespace Online_Pharmacy_Management_System.Controllers
{
    [Authorize] // ⬅️ تأكد أن المستخدم مسجل الدخول قبل السماح له بالوصول إلى أي شيء في هذا الكنترولر
    public class MedicineController : Controller
    {
        private readonly IMedicineRepository _medicineRepository;
        private readonly AppDbContext _context;

        public MedicineController(IMedicineRepository medicineRepository, AppDbContext context)
        {
            _medicineRepository = medicineRepository;
            _context = context;
        }

        [Authorize(Roles = "Admin,User")] 
        public async Task<IActionResult> Index()
        {
            var medicines = await _medicineRepository.GetAllAsync();
            var viewModel = medicines.Select(m => new MedicineViewModel
            {
                MedicineId = m.MedicineId,
                MedicineName = m.MedicineName,
                Price = m.Price,
                Description = m.Description,
                Stock = m.Stock,
                ExpiryDate = m.ExpiryDate,
                SupplierId = m.SupplierId,
                CategoryId = m.CategoryId
            }).ToList();
            return View(viewModel);
        }

        [Authorize(Roles = "Admin,User")] // ⬅️ السماح للمستخدمين العاديين بمشاهدة التفاصيل فقط
        public async Task<IActionResult> Details(int id)
        {
            var medicine = await _medicineRepository.GetByIdAsync(id);
            if (medicine == null)
                return NotFound();

            var viewModel = new MedicineViewModel
            {
                MedicineId = medicine.MedicineId,
                MedicineName = medicine.MedicineName,
                Price = medicine.Price,
                Description = medicine.Description,
                Stock = medicine.Stock,
                ExpiryDate = medicine.ExpiryDate,
                SupplierId = medicine.SupplierId,
                CategoryId = medicine.CategoryId
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")] // ⬅️ الحماية لصفحة الإضافة
        public IActionResult Add()
        {
            var model = new MedicineViewModel
            {
                Categories = _context.Categories
                    .Select(c => new SelectListItem { Value = c.CategoryId.ToString(), Text = c.Name })
                    .ToList(),
                Suppliers = _context.Suppliers
                    .Select(s => new SelectListItem { Value = s.SupplierId.ToString(), Text = s.Name })
                    .ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // ⬅️ فقط Admin يمكنه الإضافة
        public async Task<IActionResult> Add(MedicineViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _context.Categories
                    .Select(c => new SelectListItem { Value = c.CategoryId.ToString(), Text = c.Name })
                    .ToList();
                model.Suppliers = _context.Suppliers
                    .Select(s => new SelectListItem { Value = s.SupplierId.ToString(), Text = s.Name })
                    .ToList();
                return View(model);
            }

            var medicine = new Medicine
            {
                MedicineName = model.MedicineName,
                Price = model.Price,
                Description = model.Description,
                Stock = model.Stock,
                ExpiryDate = model.ExpiryDate,
                SupplierId = model.SupplierId,
                CategoryId = model.CategoryId
            };

            await _medicineRepository.AddAsync(medicine);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")] // ⬅️ حماية GET Edit
        public async Task<IActionResult> Edit(int id)
        {
            var medicine = await _medicineRepository.GetByIdAsync(id);
            if (medicine == null)
                return NotFound();

            var viewModel = new MedicineViewModel
            {
                MedicineId = medicine.MedicineId,
                MedicineName = medicine.MedicineName,
                Price = medicine.Price,
                Description = medicine.Description,
                Stock = medicine.Stock,
                ExpiryDate = medicine.ExpiryDate,
                SupplierId = medicine.SupplierId,
                CategoryId = medicine.CategoryId
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // ⬅️ فقط Admin يمكنه التعديل
        public async Task<IActionResult> Edit(int id, MedicineViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var medicine = await _medicineRepository.GetByIdAsync(id);
            if (medicine == null)
                return NotFound();

            medicine.MedicineName = model.MedicineName;
            medicine.Price = model.Price;
            medicine.Description = model.Description;
            medicine.Stock = model.Stock;
            medicine.ExpiryDate = model.ExpiryDate;
            medicine.SupplierId = model.SupplierId;
            medicine.CategoryId = model.CategoryId;

            await _medicineRepository.UpdateAsync(medicine);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")] // ⬅️ حماية GET Delete
        public async Task<IActionResult> Delete(int id)
        {
            var medicine = await _medicineRepository.GetByIdAsync(id);
            if (medicine == null)
                return NotFound();

            return View(new MedicineViewModel
            {
                MedicineId = medicine.MedicineId,
                MedicineName = medicine.MedicineName,
                Price = medicine.Price,
                Description = medicine.Description,
                Stock = medicine.Stock,
                ExpiryDate = medicine.ExpiryDate,
                SupplierId = medicine.SupplierId,
                CategoryId = medicine.CategoryId
            });
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // ⬅️ فقط Admin يمكنه الحذف
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _medicineRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
