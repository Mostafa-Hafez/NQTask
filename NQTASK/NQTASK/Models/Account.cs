using System.ComponentModel.DataAnnotations;

namespace NQTASK.Models
{
    public class Account
    {
        [Key]
        public int Acc_ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public Roles Role { get; set; }

    }


    public enum Roles
    {
        Admin,
        Client
    }
}
