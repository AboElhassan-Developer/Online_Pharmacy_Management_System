using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Pharmacy_Management_System.Models;
using Online_Pharmacy_Management_System.Repository;
using Online_Pharmacy_Management_System.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Pharmacy_Management_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var viewModel = categories.Select(c => new CategoryViewModel
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Medicines = c.Medicines.Select(m => new MedicineViewModel
                {
                    MedicineId = m.MedicineId,
                    MedicineName = m.MedicineName,
                    Price = m.Price,
                    Description = m.Description,
                    Stock = m.Stock,
                    ExpiryDate = m.ExpiryDate,
                    SupplierId = m.SupplierId,
                    CategoryId = m.CategoryId
                }).ToList()
            }).ToList();

            return View(viewModel);
        }

    
        public IActionResult Add()
        {
            return View();
        }

    
        [HttpPost]
        public async Task<IActionResult> Add(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var category = new Category
            {
                Name = model.Name
            };

            try
            {
                await _categoryRepository.AddAsync(category);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

     
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            var model = new CategoryViewModel
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var category = new Category
            {
                CategoryId = model.CategoryId,
                Name = model.Name
            };

            try
            {
                await _categoryRepository.UpdateAsync(category);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _categoryRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
