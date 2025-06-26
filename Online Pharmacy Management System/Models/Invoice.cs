using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Pharmacy_Management_System.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        [Column("Date")]
        public DateTime Date { get; set; }

        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

        public decimal TotalAmount { get; set; }
        //public decimal PaidAmount { get; set; } = 0;


        public ICollection<InvoiceItem> InvoicesItems { get; set; } = new List<InvoiceItem>();

        //public List<InvoiceItem> InvoicesItems { get; set; } = new List<InvoiceItem>();
    }
}
