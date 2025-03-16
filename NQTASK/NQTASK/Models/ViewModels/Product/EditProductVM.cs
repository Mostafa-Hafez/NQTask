using System.ComponentModel.DataAnnotations;

namespace NQTASK.Models.ViewModels.Product
{
    public class EditProductVM
    {
        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        [Required]
        [Range(5, 250)]
        public decimal Price { get; set; }
    }
}
