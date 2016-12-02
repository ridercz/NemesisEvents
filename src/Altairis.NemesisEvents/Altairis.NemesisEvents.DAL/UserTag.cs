using System.ComponentModel.DataAnnotations;

namespace Altairis.NemesisEvents.DAL {
    public class UserTag {

        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }

    }
}