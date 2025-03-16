using System;
using System.ComponentModel.DataAnnotations;

namespace NQTASK.Models.ViewModels.Client
{
    public class ClientInfo
    {

        [Display(Name ="Id")]
        public int ID { get; set; }

        [Display(Name ="Code")]
        public string Code { get; set; }
        [Display(Name ="Customer Name")]
        public string Name { get; set; }
        [Display(Name="Date Of Registraion")]
        public DateTime DateofRegistration { get; set; }
    }
}
