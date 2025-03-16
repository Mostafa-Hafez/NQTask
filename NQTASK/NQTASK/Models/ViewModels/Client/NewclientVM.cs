using System.ComponentModel.DataAnnotations;

namespace NQTASK.Models.ViewModels.Client
{
    public class NewclientVM
    {
        [Required]
        [Display(Name ="Client Code")]
        public string Code { get; set; } //unique
        [Required]
        [Display(Name ="client Name")]
        public string Name { get; set; }
        

    }
}
