using System.ComponentModel.DataAnnotations;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.DAL {
    public class UserArea : IEntity<int> {

        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int AreaId { get; set; }

        public Area Area { get; set; }

    }
}