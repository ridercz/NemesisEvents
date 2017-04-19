using System.Linq;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.DAL;
using AutoMapper;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.AutoMapper;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;

namespace Altairis.NemesisEvents.BL.Mapping {
    public class UserMapping : IMapping {

        private IUnitOfWorkProvider uowProvider;

        public UserMapping(IUnitOfWorkProvider uowProvider) {
            this.uowProvider = uowProvider;
        }

        public void Map(IMapperConfigurationExpression cfg) {
            cfg.CreateMap<User, UserDTO>();
            cfg.CreateMap<User, PublicProfileDTO>()
                .BeforeMap((s, d) =>
                {
                    // EF Core does not support lazy loading yet
                    EntityFrameworkUnitOfWork.TryGetDbContext(uowProvider)
                        .Entry(s)
                        .Collection(x => x.WatchedAreas)
                        .Load();
                    EntityFrameworkUnitOfWork.TryGetDbContext(uowProvider)
                        .Entry(s)
                        .Collection(x => x.WatchedTags)
                        .Load();
                })
                .ForMember(e => e.WatchedAreaIds, m => m.MapFrom(e => e.WatchedAreas.Select(a => a.AreaId)))
                .ForMember(e => e.WatchedTagIds, m => m.MapFrom(e => e.WatchedTags.Select(a => a.TagId)));

            cfg.CreateMap<PublicProfileDTO, User>()
                .BeforeMap((s, d) => {  // EF Core does not support lazy loading yet
                    EntityFrameworkUnitOfWork.TryGetDbContext(uowProvider).Entry(d).Collection(x => x.WatchedAreas).Load();
                    EntityFrameworkUnitOfWork.TryGetDbContext(uowProvider).Entry(d).Collection(x => x.WatchedTags).Load();
                })
                .ForMember(e => e.WatchedAreas, m => m.DropAndCreateCollection(uowProvider, x => x.WatchedAreaIds, x => new UserArea { AreaId = x }))
                .ForMember(e => e.WatchedTags, m => m.DropAndCreateCollection(uowProvider, x => x.WatchedTagIds, x => new UserTag { TagId = x }))
                .ForMember(e => e.DateCreated, m => m.Ignore())
                .ForMember(e => e.DateLastLogin, m => m.Ignore())
                .ForMember(e => e.Enabled, m => m.Ignore())
                .ForMember(e => e.Id, m => m.Ignore())
                .ForMember(e => e.UserName, m => m.Ignore())
                .ForMember(e => e.NormalizedUserName, m => m.Ignore())
                .ForMember(e => e.Email, m => m.Ignore())
                .ForMember(e => e.NormalizedEmail, m => m.Ignore())
                .ForMember(e => e.EmailConfirmed, m => m.Ignore())
                .ForMember(e => e.PasswordHash, m => m.Ignore())
                .ForMember(e => e.SecurityStamp, m => m.Ignore())
                .ForMember(e => e.ConcurrencyStamp, m => m.Ignore())
                .ForMember(e => e.PhoneNumber, m => m.Ignore())
                .ForMember(e => e.PhoneNumberConfirmed, m => m.Ignore())
                .ForMember(e => e.TwoFactorEnabled, m => m.Ignore())
                .ForMember(e => e.LockoutEnd, m => m.Ignore())
                .ForMember(e => e.LockoutEnabled, m => m.Ignore())
                .ForMember(e => e.AccessFailedCount, m => m.Ignore());

            cfg.CreateMap<User, UserBasicDTO>();
        }
    }
}
