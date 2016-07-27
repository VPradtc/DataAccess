using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using DataAccess.Core.Model.Abstraction.Interfaces;
using DataAccess.IdentityTokenCore.Model.Identity;

namespace DataAccess.Entities.Identity
{
    public class UserRole : CoreUserRole<Guid>, IEntityMap
    {
        #region Relations

        [ForeignKey("RoleId")]
        [InverseProperty("UserRoles")]
        public virtual Role Role { get; set; }

        #endregion

        #region IEntityMap

        public void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                    .HasRequired(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .WillCascadeOnDelete(false);
        }

        #endregion
    }
}
