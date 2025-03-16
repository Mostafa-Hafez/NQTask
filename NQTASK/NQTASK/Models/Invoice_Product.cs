using System.ComponentModel.DataAnnotations.Schema;

namespace NQTASK.Models
{
    public class Invoice_Product
    {
        public int In_ID { get; set; }
        [ForeignKey("In_ID")]
        public virtual Invoice invoice { get; set; }

        public int Pr_ID { get; set; }
        [ForeignKey("Pr_ID")]
        public virtual Product product { get; set; }
    }
}
