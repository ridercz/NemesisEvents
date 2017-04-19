using System.ComponentModel.DataAnnotations;

namespace Altairis.NemesisEvents.BL.DTO
{
    public class ForgottenPasswordDTO
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}