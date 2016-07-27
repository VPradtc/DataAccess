using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using DataAccess.Core.Model.Abstraction.Interfaces;
using DataAccess.Entities.Entities;
using DataAccess.Entities.Entities.BaseEntities;
using DataAccess.Entities.Identity;
using DataAccess.IdentityTokenCore.Context.Context;

namespace DataAccess.Model.Context
{
    public class DataAccessContext :
        IdentityTokenCoreContext<Guid, User, Role, UserLogin, UserRole, Claim, UserEmail, AuthClient, RefreshToken>
    {
        #region Tables

        #endregion

        #region Constructors

        public DataAccessContext(string nameOrConnectionString, bool enableProxyCreation, bool enableLazyLoading)
            : base(nameOrConnectionString)
        {
            Configuration.ProxyCreationEnabled = enableProxyCreation;
            Configuration.LazyLoadingEnabled = enableLazyLoading;
        }

        public DataAccessContext()
            : this("DefaultConnection", true, true)
        {
        }

        #endregion

        #region Static

        public static DataAccessContext CreateContext()
        {
            return new DataAccessContext();
        }

        #endregion

        #region Overrides

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var entityMapTypeof = typeof(IEntityMap);

            var typesToRegister =
                Assembly.GetAssembly(typeof(TrackableEntity))
                    .GetTypes()
                    .Where(type => entityMapTypeof.IsAssignableFrom(type) && type.IsClass)
                    .ToList();

            foreach (var typeToRegister in typesToRegister)
            {
                var mapper = Activator.CreateInstance(typeToRegister) as IEntityMap;
                mapper?.Map(modelBuilder);
            }

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
