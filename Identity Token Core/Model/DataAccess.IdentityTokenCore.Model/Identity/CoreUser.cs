using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccess.IdentityTokenCore.Model.Identity
{
    public class CoreUser<TKey, TLogin, TUserRole, TClaim, TUserEmail> :
        IdentityUser<TKey, TLogin, TUserRole, TClaim>
        where TUserEmail : CoreUserEmail<TKey>
        where TLogin : CoreUserLogin<TKey>
        where TUserRole : CoreUserRole<TKey>
        where TClaim : CoreClaim<TKey>
    {
        public virtual ICollection<TUserEmail> UserEmails { get; set; }
    }
}
