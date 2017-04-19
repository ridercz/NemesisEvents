using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.BL.DTO {

    public class AreaDTO : IEntity<int> {

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

    }
}