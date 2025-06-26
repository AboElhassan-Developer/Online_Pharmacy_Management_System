//using Microsoft.AspNetCore.Mvc;
//using Online_Pharmacy_Management_System.Models;
//using Online_Pharmacy_Management_System.Repository;
//using Online_Pharmacy_Management_System.ViewModel;
//using System.Threading.Tasks;

//namespace Online_Pharmacy_Management_System.Controllers
//{
//    public class InvoiceItemController : Controller
//    {
//        private readonly IInvoiceItemRepository _invoiceItemRepository;
//        private readonly IInvoiceRepository _invoiceRepository;

//        public InvoiceItemController(IInvoiceItemRepository invoiceItemRepository, IInvoiceRepository invoiceRepository)
//        {
//            _invoiceItemRepository = invoiceItemRepository;
//            _invoiceRepository = invoiceRepository;
//        }

//        public async Task<IActionResult> Index()
//        {
//            var invoiceItems = await _invoiceItemRepository.GetAllWithDetailsAsync();
//            return View(invoiceItems);
//        }

//        public IActionResult Create()
//        {
//            var model = new InvoiceItemViewModel();
//            return View(model);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(InvoiceItemViewModel model)
//        {
//            if (!ModelState.IsValid)
//                return View(model);

//            var invoiceItem = new InvoiceItem
//            {
//                InvoiceId = model.InvoiceId,
//                MedicineId = model.MedicineId,
//                Quantity = model.Quantity,
//                Price = model.Price
//            };

//            await _invoiceItemRepository.AddAsync(invoiceItem);
//            return RedirectToAction("Index");
//        }

//        public async Task<IActionResult> Edit(int id)
//        {
//            var invoiceItem = await _invoiceItemRepository.GetByIdAsync(id);
//            if (invoiceItem == null)
//                return NotFound();

//            var model = new InvoiceItemViewModel
//            {
//                InvoiceItemId = invoiceItem.InvoiceItemId,
//                InvoiceId = invoiceItem.InvoiceId,
//                MedicineId = invoiceItem.MedicineId,
//                Quantity = invoiceItem.Quantity,
//                Price = invoiceItem.Price
//            };

//            return View(model);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Edit(int id, InvoiceItemViewModel model)
//        {
//            if (!ModelState.IsValid)
//                return View(model);

//            var invoiceItem = await _invoiceItemRepository.GetByIdAsync(id);
//            if (invoiceItem == null)
//                return NotFound();

//            invoiceItem.Quantity = model.Quantity;
//            invoiceItem.Price = model.Price;

//            await _invoiceItemRepository.SaveAsync();
//            return RedirectToAction("Index");
//        }

//        public async Task<IActionResult> Delete(int id)
//        {
//            var invoiceItem = await _invoiceItemRepository.GetByIdAsync(id);
//            if (invoiceItem == null)
//                return NotFound();

//            return View(invoiceItem);
//        }

//        [HttpPost, ActionName("Delete")]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            await _invoiceItemRepository.DeleteAsync(id);
//            return RedirectToAction("Index");
//        }
//    }
//}
