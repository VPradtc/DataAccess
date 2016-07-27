using DataAccess.Core.Dal.Implementation.Repositories;
using DataAccess.Dal.Abstraction.Interfaces;
using DataAccess.Entities.Identity;
using System.Data.Entity;

namespace DataAccess.Dal.Repositories
{
    public class AuthClientRepository : GenericEntityRepository<AuthClient>, IAuthClientRepository
    {
        public AuthClientRepository(DbContext context) : base(context)
        {
        }
    }
}
