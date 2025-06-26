using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Online_Pharmacy_Management_System.ViewModel
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public decimal TotalAmount { get; set; }
       
       
        public List<InvoiceItemViewModel> InvoicesItems { get; set; } = new List<InvoiceItemViewModel>();
        //public List<SelectListItem> Patients { get; set; }
        //public List<SelectListItem> Medicines { get; set; }
    }
}
