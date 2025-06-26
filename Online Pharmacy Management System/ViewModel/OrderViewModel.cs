using System.ComponentModel.DataAnnotations;
using Online_Pharmacy_Management_System.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Online_Pharmacy_Management_System.ViewModel
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int? PatientId { get; set; }
        public int? SupplierId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }

        public List<SelectListItem> Patients { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Suppliers { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Medicines { get; set; } = new List<SelectListItem>();
        public List<OrderDetailViewModel> OrderDetails { get; set; } = new List<OrderDetailViewModel>();

    }
}
