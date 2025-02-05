namespace Online_Pharmacy_Management_System.Models
{
    public class Patient
    {
        public int PatientId { get; set; }  
        public string Name { get; set;}
        public string Email { get; set;}
        public string Phone { get; set;}
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set;}
        public bool IsActive { get; set; }
    }
}
