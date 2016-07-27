using System;
using DataAccess.Core.Model.Abstraction.Interfaces;
using DataAccess.IdentityTokenCore.Model.Identity;

namespace DataAccess.Entities.Identity
{
    public class UserLogin : CoreUserLogin<Guid>, IPrimary<Guid>
    {
        public Guid Id { get; set; }
    }
}
