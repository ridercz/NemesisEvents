using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Altairis.NemesisEvents.DAL {

    public class Venue {

        [Key]
        public int Id { get; set; }

        public int AreaId { get; set; }

        public virtual Area Area { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string StreetAddress { get; set; }

        public string Description { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public virtual ICollection<Event> Events { get; } = new HashSet<Event>();

    }
}