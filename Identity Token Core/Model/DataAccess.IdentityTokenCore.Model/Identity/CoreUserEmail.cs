using System.ComponentModel.DataAnnotations;

namespace DataAccess.IdentityTokenCore.Model.Identity
{
    public class CoreUserEmail<TKey>
    {
        #region Property

        [Key]
        public virtual TKey Id { get; set; }

        [MaxLength(100)]
        public virtual string Email { get; set; }

        [MaxLength(64)]
        public virtual string Provider { get; set; }

        public virtual TKey UserId { get; set; }

        #endregion Property
    }
}
