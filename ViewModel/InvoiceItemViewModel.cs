using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Online_Pharmacy_Management_System.ViewModel
{
    public class InvoiceItemViewModel
    {
        //public int InvoiceItemId { get; set; }
        //[Required]
        //public int InvoiceId { get; set; }
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
      
        public decimal Price { get; set; }
        public decimal Subtotal { get; set; }
        //public decimal Subtotal => Quantity * Price;
        //public List<SelectListItem> Medicines { get; set; } = new List<SelectListItem>();
        //public List<SelectListItem> Invoices { get; set; } = new List<SelectListItem>();
    }
}
