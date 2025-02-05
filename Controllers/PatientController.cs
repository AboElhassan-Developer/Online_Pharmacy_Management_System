
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Pharmacy_Management_System.Models;
using Online_Pharmacy_Management_System.Repository;
using Online_Pharmacy_Management_System.ViewModel;

namespace Online_Pharmacy_Management_System.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
                private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Index(string searchTerm)
        {
            var patients = await _patientRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                patients = await _patientRepository.SearchAsync(searchTerm);
            }

            var patientViewModels = patients.Select(p => new PatientViewModel
            {
                PatientId = p.PatientId,
                Name = p.Name,
                Email = p.Email,
                Phone = p.Phone,
                DateOfBirth = p.DateOfBirth,
                Address = p.Address,
                IsActive = p.IsActive
            }).ToList();

            return View(patientViewModels);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(PatientViewModel model)
        {
            if (ModelState.IsValid)
            {
                var patient = new Patient
                {
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone,
                    DateOfBirth = model.DateOfBirth,
                    Address = model.Address,
                    IsActive = model.IsActive
                };

                await _patientRepository.AddAsync(patient);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            var model = new PatientViewModel
            {
                PatientId = patient.PatientId,
                Name = patient.Name,
                Email = patient.Email,
                Phone = patient.Phone,
                DateOfBirth = patient.DateOfBirth,
                Address = patient.Address,
                IsActive = patient.IsActive
            };

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(PatientViewModel model)
        {
            if (ModelState.IsValid)
            {
                var patient = new Patient
                {
                    PatientId = model.PatientId,
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone,
                    DateOfBirth = model.DateOfBirth,
                    Address = model.Address,
                    IsActive = model.IsActive
                };

                await _patientRepository.UpdateAsync(patient);
               
                TempData["SuccessMessage"] = "Patient updated successfully!";

                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Failed to update patient. Please check the input.";
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            var model = new PatientViewModel
            {
                PatientId = patient.PatientId,
                Name = patient.Name,
                Email = patient.Email,
                Phone = patient.Phone,
                DateOfBirth = patient.DateOfBirth,
                Address = patient.Address,
                IsActive = patient.IsActive
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _patientRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
