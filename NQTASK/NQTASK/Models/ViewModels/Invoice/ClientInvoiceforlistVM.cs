using System;

namespace NQTASK.Models.ViewModels.Invoice
{
    public class ClientInvoiceforlistVM
    {
        public int In_Id { get; set; }
        public int Cl_Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OutDateTime { get; set; }
        public decimal Total { get; set; }
        
    }
}
