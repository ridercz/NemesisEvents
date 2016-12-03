using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Altairis.NemesisEvents.BL.DTO {

    public class AreaDTO {

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

    }
}