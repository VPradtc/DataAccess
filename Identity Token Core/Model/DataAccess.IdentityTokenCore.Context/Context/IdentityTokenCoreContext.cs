using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using DataAccess.IdentityTokenCore.Model.Identity;

namespace DataAccess.IdentityTokenCore.Context.Context
{
    public class IdentityTokenCoreContext<TKey, TUser, TRole, TUserLogin, TUserRole, TUserClaim, TUserEmail, TClient,
        TRefreshToken> :
            IdentityDbContext<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>
        where TRole : CoreRole<TKey, TUserRole>
        where TUserLogin : CoreUserLogin<TKey>
        where TUserRole : CoreUserRole<TKey>
        where TUserClaim : CoreClaim<TKey>
        where TUser : CoreUser<TKey, TUserLogin, TUserRole, TUserClaim, TUserEmail>
        where TUserEmail : CoreUserEmail<TKey>
        where TClient : CoreClient<TKey, TRefreshToken>
        where TRefreshToken : CoreRefreshToken<TKey>

    {
        #region Tables

        public virtual DbSet<TUserEmail> UserEmails { get; set; }

        public virtual DbSet<TClient> AuthClients { get; set; }

        public virtual DbSet<TRefreshToken> RefreshTokens { get; set; }

        #endregion

        #region Constructors

        public IdentityTokenCoreContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        #endregion
    }
}
