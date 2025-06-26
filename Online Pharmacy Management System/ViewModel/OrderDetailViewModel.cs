using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Online_Pharmacy_Management_System.ViewModel
{
    public class OrderDetailViewModel
    {
        public int OrderDetailId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int MedicineId { get; set; }

        [Required(ErrorMessage = "الكمية مطلوبة.")]
        [Range(1, int.MaxValue, ErrorMessage = "الكمية يجب أن تكون على الأقل 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "السعر مطلوب.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "السعر يجب أن يكون قيمة إيجابية.")]
        public decimal Price { get; set; }

        // خاصية جديدة للاحتفاظ بقائمة الأدوية للقائمة المنسدلة
        public List<SelectListItem> Medicines { get; set; }

        // حساب إجمالي السعر
        public decimal TotalAmount => Quantity * Price;
    }
}
