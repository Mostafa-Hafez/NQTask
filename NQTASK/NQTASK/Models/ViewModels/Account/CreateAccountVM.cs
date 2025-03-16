using System.ComponentModel.DataAnnotations;

namespace NQTASK.Models.ViewModels.Account
{
    public class CreateClientWithAccountVM
    {
        [Required(ErrorMessage ="*")]
        [Display(Name = "Client Code")]
        [MinLength(3)]
        public string Code { get; set; } //unique
        [Required(ErrorMessage = "*")]
        [Display(Name = "client Name")]
        [MinLength(4)]
        public string Name { get; set; }


        [Required(ErrorMessage = "*")]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 5,ErrorMessage = "User Name must be in (6,15) characters")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "*")]
        [RegularExpression("[A-Za-z0-9!@#$%^&*]{6,25}$",ErrorMessage ="Password Must be more than 6 character and Strong Password ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "*")]  
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password not match confirmation")]
        public string confirmPassword { get; set; }

    }
}
