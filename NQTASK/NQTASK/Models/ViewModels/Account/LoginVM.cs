using System.ComponentModel.DataAnnotations;

namespace NQTASK.Models.ViewModels.Account
{
    public class LoginVM
    {
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
       
        [Required(ErrorMessage ="*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
