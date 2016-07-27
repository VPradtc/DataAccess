
using DataAccess.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Dal.Abstraction.Restrictions
{
    public interface IEntityRoleRestrictionsProvider<TEntity>
    {
        IDictionary<RoleIdentifier, Expression<Func<TEntity, bool>>> GetRestrictions();
    }
}
