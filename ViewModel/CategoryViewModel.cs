using System.ComponentModel.DataAnnotations;

namespace Online_Pharmacy_Management_System.ViewModel
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters.")]
        public string Name { get; set; }
        public List<MedicineViewModel> Medicines { get; set; } = new List<MedicineViewModel>();
    }
}
