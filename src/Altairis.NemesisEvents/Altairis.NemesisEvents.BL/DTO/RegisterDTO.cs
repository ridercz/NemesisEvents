using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Altairis.NemesisEvents.BL.DTO
{
    public class RegisterDTO
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PasswordConfirmation { get; set; }

        [Required]
        public string FullName { get; set; }

        public string CompanyName { get; set; }

    }
}
