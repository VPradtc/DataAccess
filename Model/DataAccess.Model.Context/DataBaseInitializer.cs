using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using DataAccess.Core.Extensions.System;
using DataAccess.Entities.Enums;
using DataAccess.Entities.Identity;
using DataAccess.Model.Definition.Providers.Identity;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace DataAccess.Model.Context
{
    public class DataBaseInitializer
    {
        private UserManager<User, Guid> _userManager;
        private IRoleDefinitionProvider _roleDefinitionProvider;

        public DataBaseInitializer(DataAccessContext context)
        {
            _userManager = new UserManager<User, Guid>(new UserStore<User, Role, Guid, UserLogin, UserRole, Claim>(context));
            _userManager.UserValidator = new UserValidator<User, Guid>(_userManager) { AllowOnlyAlphanumericUserNames = false };

            _roleDefinitionProvider = new RoleDefinitionProvider();
        }
        public void Init(DataAccessContext context)
        {
            CreateRoles(context);
            CreateUser(context);
        }

        private void CreateRoles(DataAccessContext context)
        {
            Guid roleId = Guid.Empty;
            var roleDefinitions = _roleDefinitionProvider.GetAll();

            foreach (var roleDefinition in roleDefinitions)
            {
                roleId = roleId.Next();

                var role = new Role()
                {
                    Id = roleId,
                    Name = roleDefinition.Name,
                    Identifier = roleDefinition.Identifier,
                };

                context.Roles.AddOrUpdate(t => t.Name, role);
            }

            context.SaveChanges();
        }

        private void CreateUser(DataAccessContext context)
        {
            string email = "admin@ix.com";
            string role = _roleDefinitionProvider.Get(RoleIdentifier.Admin).Name;

            context.AuthClients.AddOrUpdate(client => client.Name,
                new AuthClient
                {
                    Id = Guid.NewGuid(),
                    AllowedOrigin = "http://localhost:57513",
                    Name = "DataAccessClientId",
                    Active = true,
                    ApplicationType = "1",
                    Secret = "123",
                    RefreshTokenLifeTime = 60 * 24 * 14,
                });

            if (!context.Users.Any(u => u.Email == email))
            {
                var admin = new User
                {
                    IsActive = true,
                    IsDeleted = false,
                    Email = email,
                    PhoneNumber = "000-00-00-000",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    FirstName = "admin",
                    LastName = "admin",
                };

                _userManager.Create(admin, "admin1");
                _userManager.AddToRole(admin.Id, role);
            }
            context.SaveChanges();
        }
    }

}