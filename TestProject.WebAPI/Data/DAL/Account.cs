using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace TestProject.WebAPI.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int Points { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
