using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin.Security;
using DataAccess.Entities.Identity;
using DataAccess.IdentityTokenCore.Authentication.Properties_Formater;

namespace DataAccess.Providers
{
    public class ApplicationPropertiesFormater : IdentityTokenPropertiesFormater<User, Guid>
    {
        public override AuthenticationProperties CreateProperties(User user)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                {
                    "firstName", user.FirstName
                },
                {
                    "lastName", user.LastName
                },
                {
                    "role", $"[{string.Join(",", user.Roles.Select(r => (int)r.Role.Identifier))}]"
                },
                {
                    "id", user.Id.ToString()
                }
            };

            return new AuthenticationProperties(data);
        }

    }
}