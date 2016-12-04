using System.Linq;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.DAL;
using AutoMapper;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.AutoMapper;

namespace Altairis.NemesisEvents.BL.Mapping {
    public class UserMapping : IMapping {

        private IUnitOfWorkProvider uowProvider;

        public UserMapping(IUnitOfWorkProvider uowProvider) {
            this.uowProvider = uowProvider;
        }

        public void Map(IMapperConfigurationExpression cfg) {
            cfg.CreateMap<User, UserDTO>();
            cfg.CreateMap<User, PublicProfileDTO>()
                .ForMember(e => e.WatchedAreaIds, m => m.MapFrom(e => e.WatchedAreas.Select(a => a.AreaId)))
                .ForMember(e => e.WatchedTagIds, m => m.MapFrom(e => e.WatchedTags.Select(a => a.TagId)));
            cfg.CreateMap<PublicProfileDTO, User>()
                .ForMember(e => e.WatchedAreas, m => m.DropAndCreateCollection(uowProvider, x => x.WatchedAreaIds, x => new UserArea { AreaId = x }))
                .ForMember(e => e.WatchedTags, m => m.DropAndCreateCollection(uowProvider, x => x.WatchedTagIds, x => new UserTag { TagId = x }));

        }
    }
}
