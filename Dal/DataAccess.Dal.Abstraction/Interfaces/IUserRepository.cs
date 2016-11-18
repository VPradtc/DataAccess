using System;
using System.Threading.Tasks;
using DataAccess.Core.Dal.Abstraction.Interfaces.Repositories;
using DataAccess.Entities.Identity;
using Microsoft.AspNet.Identity;
using DataAccess.Entities.Enums;

namespace DataAccess.Dal.Abstraction.Interfaces
{
    public interface IUserRepository : ITrackableEntityRepository<Guid, User, Guid?>
    {
        Task<bool> IsUniqueEmailAsync(string username);
        Task<User> CreateAsync(User entity, string password, RoleIdentifier roleIdentifier);
        Task<User> UpdateAsync(User entity, RoleIdentifier roleIdentifier);
    }
}
