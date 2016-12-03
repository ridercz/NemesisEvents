using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Altairis.NemesisEvents.BL.DTO {

    public class VenueDTO {

        public int Id { get; set; }

        public string AreaName { get; set; }

        public string City { get; set; }

        public string Name { get; set; }

        public string StreetAddress { get; set; }

        public bool HasGpsCoordinates { get; set; }

    }
}