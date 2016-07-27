using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading;
using DataAccess.Dal.Abstraction.Interfaces;
using DataAccess.Dal.Repositories.Base;
using DataAccess.Entities.Enums;
using DataAccess.Entities.Identity;
using DataAccess.Model.Context;

namespace DataAccess.Dal.Repositories
{
    public class RoleRepository : PrimaryEntityPrimaryRepository<Role>, IRoleRepository
    {
        #region Constructors

        public RoleRepository(DbContext context) : base(context)
        {
            Permissions = new Dictionary<RoleIdentifier, Expression<Func<Role, bool>>>();
        }

        public Role GetByName(string name)
        {
            return Query.FirstOrDefault(r => r.Name == name);
        }

        public Role GetByIdentifier(RoleIdentifier identifier)
        {
            return Query.FirstOrDefault(r => r.Identifier == identifier);
        }

        #endregion

        #region StaticMethods

        private static List<Role> _roles;
        public static RoleIdentifier GetLoggedUserMaxRole()
        {
            if (_roles == null || _roles.Count == 0)
            {
                _roles = new DataAccessContext().Roles.ToList();
            }

            var userRoles = ((ClaimsIdentity)Thread.CurrentPrincipal.Identity).Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            return userRoles.Select(r => _roles.FirstOrDefault(t => t.Name == r)).Select(r => r.Identifier).OrderByDescending(r => r).FirstOrDefault();
        }

        #endregion StaticMethods
    }
}
