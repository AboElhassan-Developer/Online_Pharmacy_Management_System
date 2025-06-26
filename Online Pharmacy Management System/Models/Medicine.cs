using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Pharmacy_Management_System.Models
{
    public class Medicine
    {
        [Key]
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
        public int Stock { get; set; } // الكمية المتوفرة في المخزون
        [Required]
        [DataType(DataType.Date)]
      
        public DateTime ExpiryDate { get; set; }

        public bool IsAvailable => Stock > 0; // متاح إذا كان المخزون أكبر من 0

        [Required]
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public override string ToString()
        {
            return $"{MedicineName} - {Price:C} - {Stock} in stock - Expiry: {ExpiryDate.ToShortDateString()}";
        }
    }
}
