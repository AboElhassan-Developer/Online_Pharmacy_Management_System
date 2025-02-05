using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Pharmacy_Management_System.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [ForeignKey("Patient")]
        public int? PatientId { get; set; } // Nullable if the order is from the pharmacy itself
        public Patient? Patient { get; set; }

        [ForeignKey("Supplier")]
        public int? SupplierId { get; set; } // Nullable if the order is from a patient
        public Supplier? Supplier { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending"; // Default status

        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    }
}
