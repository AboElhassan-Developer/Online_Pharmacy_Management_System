namespace Online_Pharmacy_Management_System.ViewModel
{
    public class EditUserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string CurrentRole { get; set; }

        public List<string> Roles { get; set; } = new List<string>();
        public string SelectedRole { get; set; }
    }
}
