using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Dal.Abstraction.Interfaces;
using DataAccess.Dal.Repositories.Base;
using DataAccess.Entities.Enums;
using DataAccess.Entities.Identity;
using Microsoft.AspNet.Identity;
using DataAccess.Model.Definition.Providers.Identity;
using DataAccess.Dal.Identity;

namespace DataAccess.Dal.Repositories
{
    public class UserRepository : TrackableEntityPrimaryRepository<User, Guid?>, IUserRepository
    {
        private readonly IRoleDefinitionProvider _roleDefinitionProvider;
        private readonly ApplicationUserManager _userManager;

        private readonly Lazy<User> _currentUser;

        public User CurrentUser => _currentUser.Value;

        #region Constructors

        public UserRepository(DbContext context,
            IRoleDefinitionProvider roleDefinitionProvider,
            ApplicationUserManager userManager)
            : this(context)
        {
            _roleDefinitionProvider = roleDefinitionProvider;
            _userManager = userManager;
            _currentUser = new Lazy<User>(GetCurrentUser);
        }

        public UserRepository(DbContext context) : base(context)
        {
            Permissions = new Dictionary<RoleIdentifier, Expression<Func<User, bool>>>
            {
                {
                    RoleIdentifier.Admin, u =>
                        u.Roles.All(r => r.Role.Identifier != RoleIdentifier.Admin)
                }
            };
        }

        public async Task<User> CreateAsync(User entity, string password, RoleIdentifier roleIdentifier)
        {
            var dbEntity = await base.CreateAsync(entity);

            await AddPasswordAsync(entity.Id, password);

            await AddRoleAsync(entity.Id, roleIdentifier);

            return dbEntity;
        }

        protected User GetCurrentUser()
        {
            if(!CurrentUserId.HasValue)
            {
                return null;
            }

            var user = GetById(CurrentUserId.Value);

            return user;
        }

        public async Task<User> UpdateAsync(User entity, RoleIdentifier roleIdentifier)
        {
            var dbEntity = await base.UpdateAsync(entity);

            await SetRoleAsync(entity.Id, roleIdentifier);

            return dbEntity;
        }

        public async Task<bool> IsUniqueEmailAsync(string email)
        {
            var exists = await Query.AnyAsync(item => item.Email == email);
            return !exists;
        }

        public async Task<IdentityResult> AddRoleAsync(Guid userId, RoleIdentifier roleIdentifier)
        {
            var role = _roleDefinitionProvider.Get(roleIdentifier);
            return await _userManager.AddToRoleAsync(userId, role.Name);
        }

        public async Task<IdentityResult> SetRoleAsync(Guid userId, RoleIdentifier roleIdentifier)
        {
            var rolesToRemove = (await _userManager.GetRolesAsync(userId)).ToArray();
            await _userManager.RemoveFromRolesAsync(userId, rolesToRemove);

            return await AddRoleAsync(userId, roleIdentifier);
        }

        public async Task<IdentityResult> AddPasswordAsync(Guid userId, string password)
        {
            var result = await _userManager.AddPasswordAsync(userId, password);
            return result;
        }

        #endregion
    }
}
