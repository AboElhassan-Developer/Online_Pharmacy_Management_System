using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Online_Pharmacy_Management_System.ViewModel
{
    public class MedicineViewModel
    {
        public int MedicineId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Medicine name cannot exceed 100 characters.")]
        public string MedicineName { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
        public int Stock { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public bool IsAvailable => Stock > 0;

        // **إضافة القوائم المنسدلة**
        public List<SelectListItem> Categories { get; set; } = new();
        public List<SelectListItem> Suppliers { get; set; } = new();
    }
}
