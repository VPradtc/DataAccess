using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccess.IdentityTokenCore.Model.Identity
{
    public class CoreRole<TKey, TUserRole> : IdentityRole<TKey, TUserRole>
        where TUserRole : CoreUserRole<TKey>

    {
    }
}
