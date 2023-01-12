
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage="RegisterEmail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
