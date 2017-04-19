using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.BL.DTO {
    public class VenueDetailDTO : IEntity<int> {

        [Key]
        public int Id { get; set; }

        public int AreaId { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string StreetAddress { get; set; }

        public string Description { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

    }
}
