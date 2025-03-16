using System;
using System.ComponentModel.DataAnnotations;

namespace NQTASK.Models.ViewModels.Client
{
    public class DetailsClientVM
    {

        
        [Display(Name = "Client Code")]
        public string Code { get; set; } 
        
        [Display(Name = "client Name")]
        public string Name { get; set; }

        [Display(Name="DateTime Of Registraion")]
        public DateTime DatetimeofRegistration { get; set; }
    }
}
