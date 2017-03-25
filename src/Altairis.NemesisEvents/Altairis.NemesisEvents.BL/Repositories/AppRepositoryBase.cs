using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.DAL;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;

namespace Altairis.NemesisEvents.BL.Repositories
{
    public class AppRepositoryBase<TEntity, TKey> : EntityFrameworkRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>, new()
    {

        public new NemesisEventsContext Context => (NemesisEventsContext)base.Context;

        public AppRepositoryBase(IUnitOfWorkProvider provider, IDateTimeProvider dateTimeNowProvider) : base(provider, dateTimeNowProvider)
        {
        }
    }
}
