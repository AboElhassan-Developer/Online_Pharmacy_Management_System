using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Pharmacy_Management_System.Models
{
    public class InvoiceItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        public Invoice Invoice { get; set; }

        [Required]
        public int MedicineId { get; set; }

        [ForeignKey("MedicineId")]
        public Medicine Medicine { get; set; }
        [Required]
        public int Quantity { get; set; }
        public decimal Price { get; set; }

   
        public decimal Subtotal => Quantity * Price;

     
       
    }
}
