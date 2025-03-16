using System.ComponentModel.DataAnnotations;

namespace NQTASK.Models.ViewModels.Product
{
    public class ProductforListVM
    {
        public int Id { get; set; }

        [Display(Name = "Product Name")]
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
