using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Online_Pharmacy_Management_System.Models;
using Online_Pharmacy_Management_System.Repository;
using Online_Pharmacy_Management_System.ViewModel;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Online_Pharmacy_Management_System.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMedicineRepository _medicineRepository;

        public OrderController(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository,
                               IPatientRepository patientRepository, ISupplierRepository supplierRepository,
                               IMedicineRepository medicineRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _patientRepository = patientRepository;
            _supplierRepository = supplierRepository;
            _medicineRepository = medicineRepository;
        }


        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Index()
        {
            var orders = await _orderRepository.GetAllAsync();
            return View(orders);
        }
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return NotFound();
            return View(order);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var viewModel = new OrderViewModel
            {
                Patients = (await _patientRepository.GetAllAsync()).Select(p => new SelectListItem { Value = p.PatientId.ToString(), Text = p.Name }).ToList(),
                Suppliers = (await _supplierRepository.GetAllAsync()).Select(s => new SelectListItem { Value = s.SupplierId.ToString(), Text = s.Name }).ToList(),
                Medicines = (await _medicineRepository.GetAllAsync()).Select(m => new SelectListItem { Value = m.MedicineId.ToString(), Text = m.MedicineName }).ToList(),
                OrderDate = DateTime.Now,
                Status = "Pending"
            };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(OrderViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var order = new Order
            {
                PatientId = model.PatientId,
                SupplierId = model.SupplierId,
                OrderDate = model.OrderDate,
                Status = model.Status,
                TotalAmount = model.TotalAmount
            };
            await _orderRepository.AddAsync(order);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return NotFound();

            var viewModel = new OrderViewModel
            {
                PatientId = order.PatientId,
                SupplierId = order.SupplierId,
                OrderDate = order.OrderDate,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                Patients = (await _patientRepository.GetAllAsync()).Select(p => new SelectListItem
                {
                    Value = p.PatientId.ToString(),
                    Text = p.Name
                }).ToList(),
                Suppliers = (await _supplierRepository.GetAllAsync()).Select(s => new SelectListItem
                {
                    Value = s.SupplierId.ToString(),
                    Text = s.Name
                }).ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, OrderViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return NotFound();

            order.PatientId = model.PatientId;
            order.SupplierId = model.SupplierId;
            order.OrderDate = model.OrderDate;
            order.Status = model.Status;
            order.TotalAmount = model.TotalAmount;

            await _orderRepository.UpdateAsync(order);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return NotFound();
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _orderRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddOrderDetail(int orderId)
        {
            var viewModel = new OrderDetailViewModel
            {
                OrderId = orderId,
                Medicines = (await _medicineRepository.GetAllAsync()).Select(m => new SelectListItem
                {
                    Value = m.MedicineId.ToString(),
                    Text = m.MedicineName
                }).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddOrderDetail(OrderDetailViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var orderDetail = new OrderDetail
            {
                OrderId = model.OrderId,
                MedicineId = model.MedicineId,
                Quantity = model.Quantity,
                Price = model.Price
            };

            await _orderDetailRepository.AddAsync(orderDetail);
            return RedirectToAction("Details", new { id = model.OrderId });
        }
    }
}

