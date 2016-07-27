using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Threading;
using Microsoft.AspNet.Identity;
using DataAccess.Core.Model.Abstraction.Interfaces;
using DataAccess.Entities.Entities;
using DataAccess.IdentityTokenCore.Model.Identity;

namespace DataAccess.Entities.Identity
{
    public class User :
        CoreUser<Guid, UserLogin, UserRole, Claim, UserEmail>,
        IPrimary<Guid>, IStatable, ITrackable<Guid?>, IEntityMap
    {
        #region IPrimary

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override Guid Id { get; set; }

        #endregion IPrimary

        #region IStatable

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        #endregion

        #region ITrackable

        public DateTime CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }

        #endregion

        #region Properties

        [Index(IsUnique = true)]
        [EmailAddress]
        [Required]
        public override string Email
        {
            get
            {
                return base.Email;
            }

            set
            {
                base.Email = value;
            }
        }

        public override string UserName
        {
            get
            {
                return Email;
            }

            set
            {
                Email = value;
            }
        }

        [Column("FirstName")]
        [Required]
        public string FirstName { get; set; }

        [Column("LastName")]
        [Required]
        public string LastName { get; set; }

        #endregion

        #region Relations

        [ForeignKey("CreatedBy")]
        [InverseProperty("CreatedUsers")]
        public virtual User CreatedByUser { get; set; }

        [ForeignKey("ModifiedBy")]
        [InverseProperty("ModifiedUsers")]
        public virtual User ModifiedByUser { get; set; }

        #endregion

        #region Created / Modified

        public virtual ICollection<User> CreatedUsers { get; set; }
        public virtual ICollection<User> ModifiedUsers { get; set; }

        #endregion

        #region IEntityMap

        public void Map(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
        }

        #endregion IEntityMap

        public User()
        {
            Guid userId;
            var userIdExists =  Guid.TryParse(Thread.CurrentPrincipal.Identity.GetUserId(), out userId);

            Guid? actualUserId = userIdExists ? userId : default(Guid?);

            CreatedBy = actualUserId;
            ModifiedBy = actualUserId;

            CreatedDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;
        }
    }
}
