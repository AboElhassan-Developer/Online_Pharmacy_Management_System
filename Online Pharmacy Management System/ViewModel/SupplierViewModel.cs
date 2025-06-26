using System.ComponentModel.DataAnnotations;

namespace Online_Pharmacy_Management_System.ViewModel
{
    public class SupplierViewModel
    {
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Supplier name is required.")]
        [StringLength(100, ErrorMessage = "Supplier name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Contact number is required.")]
        [StringLength(20, ErrorMessage = "Contact number cannot exceed 20 characters.")]
        [Phone(ErrorMessage = "Invalid contact number format.")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string Address { get; set; }

        public List<MedicineViewModel> Medicines { get; set; } = new List<MedicineViewModel>();
    }
}
