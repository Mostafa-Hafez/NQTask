using System.ComponentModel.DataAnnotations;

namespace NQTASK.Models.ViewModels.Product
{
    public class ProductInfoVM
    {
        [Display(Name= "Product Name")]
        public string Name { get; set; }

        public decimal Price { get; set; }

    }
}
