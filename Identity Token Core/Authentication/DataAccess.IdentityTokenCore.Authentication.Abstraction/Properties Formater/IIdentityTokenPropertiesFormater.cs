using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace DataAccess.IdentityTokenCore.Authentication.Abstraction.Properties_Formater
{
    public interface IIdentityTokenPropertiesFormater<in TUser, TKey>
        where TUser : class, IUser<TKey>
    {
        AuthenticationProperties CreateProperties(TUser user);
    }
}
