using System.ComponentModel.DataAnnotations;

namespace DataAccess.IdentityTokenCore.Model.Identity
{
    public class CoreRefreshToken<TKey>
    {
        #region Property

        [Key]
        public virtual TKey Id { get; set; }

        [MaxLength(256)]
        public virtual string Subject { get; set; }

        public virtual TKey ClientId { get; set; }

        [MaxLength(128)]
        public virtual string IssuedUtc { get; set; }

        [MaxLength(128)]
        public virtual string ExpiresUtc { get; set; }

        [MaxLength(1024)]
        public virtual string ProtectedTicket { get; set; }

        #endregion Property
    }
}
