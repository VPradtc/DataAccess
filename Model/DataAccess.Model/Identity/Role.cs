using System;
using System.Collections.Generic;
using DataAccess.Core.Model.Abstraction.Interfaces;
using DataAccess.Entities.Enums;
using DataAccess.IdentityTokenCore.Model.Identity;

namespace DataAccess.Entities.Identity
{
    public class Role : CoreRole<Guid, UserRole>, IPrimary<Guid>
    {
        public RoleIdentifier Identifier { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
