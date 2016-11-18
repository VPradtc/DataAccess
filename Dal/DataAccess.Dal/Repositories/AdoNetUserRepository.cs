using DataAccess.Dal.Abstraction.Interfaces;
using DataAccess.Dal.Identity;
using DataAccess.Entities.Enums;
using DataAccess.Entities.Identity;
using DataAccess.Model.Definition.Providers.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Dal.Repositories
{
    public class AdoNetUserRepository : IUserRepository
    {
        #region Dead methods

        public User Update(User entity)
        {
            throw new NotImplementedException();
        }

        public User Activate(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<User> ActivateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public User Create(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<User> CreateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public User Deactivate(User entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetActive(Expression<Func<User, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public User GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public IQueryable<User> GetInactive(Expression<Func<User, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        #endregion

        private readonly string _connectionString;
        private readonly ApplicationUserManager _userManager;
        private readonly IRoleDefinitionProvider _roleProvider;

        public AdoNetUserRepository(ApplicationUserManager userManager,
            IRoleDefinitionProvider roleProvider)
        {
            _userManager = userManager;
            _roleProvider = roleProvider;

            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private User CreateUser(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        private void RunCommand(string query)
        {
            var cleanQuery = query.Replace("True", "1").Replace("False", "0");
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(cleanQuery, connection);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private IEnumerable<User> RunQuery(string query)
        {
            var result = new List<User>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var entity = new User
                    {
                        Id = (Guid)reader["Id"],
                        CreatedDate = (DateTime)reader["CreatedDate"],
                        ModifiedDate = (DateTime)reader["ModifiedDate"],
                        Email = (string)reader["Email"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        PhoneNumber = (string)reader["PhoneNumber"],
                        IsActive = (bool)reader["IsActive"],
                    };

                    entity.Roles.Add(new UserRole()
                    {
                        Role = new Role
                        {
                            Identifier = (RoleIdentifier)reader["Role"],
                        },
                    });

                    result.Add(entity);
                }
                reader.Close();
            }

            return result;
        }

        public async Task<User> DeactivateAsync(User entity)
        {
            string queryString = String.Format("UPDATE AspNetUsers SET IsActive = 0 WHERE Id = '{0}'", entity.Id);

            RunCommand(queryString);

            return entity;
        }

        public async Task<User> CreateAsync(User entity, string password, RoleIdentifier roleIdentifier)
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.ModifiedDate = DateTime.UtcNow;
            entity.IsActive = true;

            var command = String.Format(" INSERT INTO AspNetUsers (CreatedDate, ModifiedDate, Email, FirstName, LastName, PhoneNumber, LockoutEnabled, AccessFailedCount, TwoFactorEnabled, PhoneNumberConfirmed, EmailConfirmed, IsActive, IsDeleted, UserName)"
                        + " VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7}, {8}, {9}, {10}, {11}, {12}, '{13}')",
                        entity.CreatedDate,
                        entity.ModifiedDate,
                        entity.Email,
                        entity.FirstName,
                        entity.LastName,
                        entity.PhoneNumber,
                        entity.LockoutEnabled,
                        entity.AccessFailedCount,
                        entity.TwoFactorEnabled,
                        entity.PhoneNumberConfirmed,
                        entity.EmailConfirmed,
                        entity.IsActive,
                        entity.IsDeleted,
                        entity.Email);
            RunCommand(command);

            var dbUser = await _userManager.FindByEmailAsync(entity.Email);
            var roleName = _roleProvider.Get(roleIdentifier).Name;

            await _userManager.AddPasswordAsync(dbUser.Id, password);
            await _userManager.AddToRoleAsync(dbUser.Id, roleName);

            return dbUser;
        }

        public IQueryable<User> Get(Expression<Func<User, bool>> filter = null)
        {
            var query = " SELECT u.*, r.Identifier AS Role FROM AspNetUsers u"
                      + " JOIN AspNetUserRoles ur ON u.Id = ur.UserId"
                      + " JOIN AspNetRoles r ON r.Id = ur.RoleId";

            var result = RunQuery(query).AsQueryable().Where(u => u.IsActive);

            if (filter != null)
            {
                return result.Where(filter);
            }

            return result;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = Get(u => u.Id == id).FirstOrDefault();

            return user;
        }

        public async Task<bool> IsUniqueEmailAsync(string email)
        {
            var exists = Get(u => u.Email == email).Any();

            return !exists;
        }

        public async Task<User> UpdateAsync(User entity, RoleIdentifier roleIdentifier)
        {
            entity.ModifiedDate = DateTime.UtcNow;

            var command = String.Format(" UPDATE AspNetUsers"
                        + " SET ModifiedDate = '{1}', Email = '{2}', FirstName = '{3}', LastName='{4}', PhoneNumber='{5}'"
                        + " WHERE Id = '{6}'",
                        entity.CreatedDate,
                        entity.ModifiedDate,
                        entity.Email,
                        entity.FirstName,
                        entity.LastName,
                        entity.PhoneNumber,
                        entity.Id);

            RunCommand(command);

            var rolesToRemove = (await _userManager.GetRolesAsync(entity.Id)).ToArray();
            await _userManager.RemoveFromRolesAsync(entity.Id, rolesToRemove);

            var roleName = _roleProvider.Get(roleIdentifier).Name;
            await _userManager.AddToRoleAsync(entity.Id, roleName);

            return entity;
        }
    }
}
