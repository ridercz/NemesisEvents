namespace Altairis.NemesisEvents.BL.DTO
{
    public class VenueBasicDTO
    {
        public int Id { get; set; }

        public string City { get; set; }

        public string Name { get; set; }

        public string DisplayName => string.Join(" - ", this.City, this.Name);
    }
}