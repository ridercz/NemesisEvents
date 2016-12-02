using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Altairis.NemesisEvents.DAL {

    public class Area {

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Venue> Venues { get; set; }

    }
}