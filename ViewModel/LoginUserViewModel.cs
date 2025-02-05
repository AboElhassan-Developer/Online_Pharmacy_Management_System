using System.ComponentModel.DataAnnotations;

namespace Online_Pharmacy_Management_System.ViewModel
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage ="*")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
