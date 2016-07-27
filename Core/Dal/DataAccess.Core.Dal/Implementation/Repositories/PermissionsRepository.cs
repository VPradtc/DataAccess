using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Core.Dal.Implementation.Repositories
{
    public class PermissionsRepository< TEntity, TPermissionsKey> :
        GenericEntityRepository<TEntity>
        where TEntity : class
    {
        protected IDictionary<TPermissionsKey, Expression<Func<TEntity, bool>>> Permissions;

        #region Constructor

        public PermissionsRepository(DbContext context) : base(context)
        {
        }

        #endregion Constructor

        public override IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null)
        {
            return base.Get(predicate).Where(GetFilteringByPermissions(Permissions));
        }

        #region Async

        #endregion Default

        public virtual Expression<Func<TEntity, bool>> GetFilteringByPermissions(IDictionary<TPermissionsKey, Expression<Func<TEntity, bool>>> permissions)
        {
            if (permissions != null && permissions.ContainsKey(CurentPermission))
            {
                return permissions[CurentPermission];
            }
            return x => true;
        }

        protected virtual TPermissionsKey CurentPermission
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
