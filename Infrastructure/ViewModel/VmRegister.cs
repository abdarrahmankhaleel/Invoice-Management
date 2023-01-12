using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModel
{
    public class VmRegister
    {
        [Required(ErrorMessage ="you must insert name")]
        [MaxLength(20)]
        [MinLength(3)]
        public string Name { get; set; }
        [Required(ErrorMessage="select ypur branch")]
        public int BranchId { get; set; }
        [Required(ErrorMessage="you must enter email")]
        [EmailAddress(ErrorMessage="must be a valid email")]
        public string Email { get; set; }
        [Required(ErrorMessage="enter password")]
        [MaxLength(20)]
        [MinLength(5)]
        public string Password { get; set; }
        [Required(ErrorMessage = "enter confirm password")]
        [Compare("Password", ErrorMessage = "must be the same as password")]
        public string ComparePassword { get; set; }
    }
}
