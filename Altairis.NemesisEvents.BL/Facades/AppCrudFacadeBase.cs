using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.Services.Facades;
using Riganti.Utils.Infrastructure;

namespace Altairis.NemesisEvents.BL.Facades {
    public class AppCrudFacadeBase<TEntity, TKey, TListDTO, TDetailDTO> : CrudFacadeBase<TEntity, TKey, TListDTO, TDetailDTO>, ICrudFacade<TDetailDTO, TKey>, IDataSourceFacade<TListDTO>
        where TEntity : IEntity<TKey>
        where TDetailDTO : IEntity<TKey> {

        public AppCrudFacadeBase(Func<IQuery<TListDTO>> queryFactory, IRepository<TEntity, TKey> repository, IEntityDTOMapper<TEntity, TDetailDTO> mapper) : base(queryFactory, repository, mapper) {
        }
        public virtual void FillDataSet(GridViewDataSet<TListDTO> dataSet) {
            if (dataSet == null) throw new ArgumentNullException(nameof(dataSet));

            using (var uow = this.UnitOfWorkProvider.Create()) {
                var q = this.QueryFactory();
                dataSet.LoadFromQuery(q);
            }
        }
    }
}
