using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using DataAccess.Entities.Identity;
using Ninject;

namespace DataAccess.Dal.Identity
{
    public class ApplicationUserManager : UserManager<User, Guid>
    {
        private static IDataProtectionProvider _dataProtectionProvider;

        [Inject]
        public ApplicationUserManager(IUserStore<User, Guid> store) : base(store)
        {
            UserValidator = new UserValidator<User, Guid>(this) { AllowOnlyAlphanumericUserNames = false };
            UserTokenProvider = new DataProtectorTokenProvider<User, Guid>(_dataProtectionProvider.Create("ResetPasswordPurpose"))
            {
                TokenLifespan = TimeSpan.FromDays(14)
            };
        }

        public static void SetDataProtectionProvider(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtectionProvider = dataProtectionProvider;
        }
    }
}
