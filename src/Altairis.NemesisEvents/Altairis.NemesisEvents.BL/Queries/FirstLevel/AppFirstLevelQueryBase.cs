using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.DAL;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;

namespace Altairis.NemesisEvents.BL.Queries.FirstLevel
{
    public abstract class AppFirstLevelQueryBase<TResult> : EntityFrameworkFirstLevelQueryBase<TResult> where TResult : class
    {
        public new NemesisEventsContext Context => (NemesisEventsContext)base.Context;

        public AppFirstLevelQueryBase(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {
        }
    }
}
