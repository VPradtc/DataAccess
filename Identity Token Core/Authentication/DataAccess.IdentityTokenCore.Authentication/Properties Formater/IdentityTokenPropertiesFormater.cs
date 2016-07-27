using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using DataAccess.IdentityTokenCore.Authentication.Abstraction.Properties_Formater;

namespace DataAccess.IdentityTokenCore.Authentication.Properties_Formater
{
    public class IdentityTokenPropertiesFormater<TUser, TKey> :
        IIdentityTokenPropertiesFormater<TUser, TKey>
        where TUser : class, IUser<TKey>
    {
        #region Public Methods

        public IdentityTokenPropertiesFormater() { }

        public virtual AuthenticationProperties CreateProperties(TUser user)
        {
            return new AuthenticationProperties();
        }

        #endregion
    }
}
