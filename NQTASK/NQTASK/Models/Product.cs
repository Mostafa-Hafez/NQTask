using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NQTASK.Models
{
    public class Product
    {
        [Key]
        public int Pr_ID  { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual List<Invoice_Product> InvoicesProducts { get; set; }

    }
}
