using Microsoft.AspNetCore.Mvc;
using Online_Pharmacy_Management_System.Models;
using Online_Pharmacy_Management_System.Repository;
using Online_Pharmacy_Management_System.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Pharmacy_Management_System.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceItemRepository _invoiceItemRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMedicineRepository _medicineRepository;

        public InvoiceController(IInvoiceRepository invoiceRepository, IInvoiceItemRepository invoiceItemRepository, IPatientRepository patientRepository, IMedicineRepository medicineRepository)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceItemRepository = invoiceItemRepository;
            _patientRepository = patientRepository;
            _medicineRepository = medicineRepository;
        }

        public async Task<IActionResult> Index()
        {
            var invoices = await _invoiceRepository.GetAllAsync();
            return View(invoices);
        }

        public async Task<IActionResult> Details(int id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            if (invoice == null)
                return NotFound();

            return View(invoice);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new InvoiceViewModel
            {
                InvoicesItems = new List<InvoiceItemViewModel>
                {
                    new InvoiceItemViewModel()
                }
            };

            // تحميل قائمة المرضى والأدوية
            ViewBag.Patients = await _patientRepository.GetAllAsync();
            ViewBag.Medicines = await _medicineRepository.GetAllAsync();

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvoiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var invoice = new Invoice
                {
                    Date = model.Date,
                    PatientId = model.PatientId,
                    TotalAmount = model.TotalAmount,
                    InvoicesItems = model.InvoicesItems.Select(item => new InvoiceItem
                    {
                        MedicineId = item.MedicineId,
                        Quantity = item.Quantity,
                        Price = item.Price
                    }).ToList()
                };

                await _invoiceRepository.AddAsync(invoice);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            if (invoice == null)
                return NotFound();

            ViewBag.Patients = await _patientRepository.GetAllAsync();
            ViewBag.Medicines = await _medicineRepository.GetAllAsync();
            return View(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Invoice invoice, List<InvoiceItem> items)
        {
            if (ModelState.IsValid)
            {
                invoice.TotalAmount = items.Sum(i => i.Subtotal);
                invoice.InvoicesItems = items;
                await _invoiceRepository.UpdateAsync(invoice);
                return RedirectToAction("Index");
            }
            ViewBag.Patients = await _patientRepository.GetAllAsync();
            ViewBag.Medicines = await _medicineRepository.GetAllAsync();
            return View(invoice);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            if (invoice == null)
                return NotFound();

            return View(invoice);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _invoiceRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
