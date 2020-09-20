using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace TestProject.WebAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "decimal(16,2)")]
        public decimal Salary{ get; set; } // monthly
        [Column(TypeName = "decimal(16,2)")]
        public decimal Expenses { get; set; } // monthly
        public List<Account> Accounts { get; set; }
    }
}
