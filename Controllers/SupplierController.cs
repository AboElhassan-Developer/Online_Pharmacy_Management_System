using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Pharmacy_Management_System.Models;
using Online_Pharmacy_Management_System.Repository;
using Online_Pharmacy_Management_System.ViewModel;

namespace Online_Pharmacy_Management_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SupplierController : Controller
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IOrderRepository _orderRepository;

        public SupplierController(ISupplierRepository supplierRepository, IOrderRepository orderRepository)
        {
            _supplierRepository = supplierRepository;
            _orderRepository = orderRepository;
        }

        // عرض جميع الموردين
        public async Task<IActionResult> Index()
        {
            var suppliers = await _supplierRepository.GetAllAsync();
            var supplierViewModels = suppliers.Select(s => new SupplierViewModel
            {
                SupplierId = s.SupplierId,
                Name = s.Name,
                Contact = s.Contact,
                Email = s.Email,
                Address = s.Address
            }).ToList();

            return View(supplierViewModels);
        }
        public IActionResult Add()
        {
            return View();
        }

        // حفظ المورد الجديد
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(SupplierViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var supplier = new Supplier
            {
                Name = model.Name,
                Contact = model.Contact,
                Email = model.Email,
                Address = model.Address
            };

            await _supplierRepository.AddAsync(supplier);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            var model = new SupplierViewModel
            {
                SupplierId = supplier.SupplierId,
                Name = supplier.Name,
                Contact = supplier.Contact,
                Email = supplier.Email,
                Address = supplier.Address
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SupplierViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var supplier = await _supplierRepository.GetByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            supplier.Name = model.Name;
            supplier.Contact = model.Contact;
            supplier.Email = model.Email;
            supplier.Address = model.Address;

            await _supplierRepository.UpdateAsync(supplier);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            // تحقق إذا كان المورد مرتبطًا بأوامر
            //var hasOrders = await _orderRepository.ExistsBySupplierIdAsync(id);
            //if (hasOrders)
            //{
            //    TempData["Error"] = "Cannot delete supplier as there are orders associated with it.";
            //    return RedirectToAction(nameof(Index));
            //}

            return View(new SupplierViewModel
            {
                SupplierId = supplier.SupplierId,
                Name = supplier.Name,
                Contact = supplier.Contact,
                Email = supplier.Email,
                Address = supplier.Address
            });
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _supplierRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
