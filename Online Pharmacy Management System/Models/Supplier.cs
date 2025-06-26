namespace Online_Pharmacy_Management_System.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        
        public List<Medicine> Medicines { get; set; } = new List<Medicine>();
    }
}
