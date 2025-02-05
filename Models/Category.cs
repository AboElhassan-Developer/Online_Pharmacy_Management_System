namespace Online_Pharmacy_Management_System.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public List<Medicine> Medicines { get; set; } = new List<Medicine>();
    }
}
