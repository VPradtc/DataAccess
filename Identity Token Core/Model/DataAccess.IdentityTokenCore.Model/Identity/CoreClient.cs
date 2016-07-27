using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.IdentityTokenCore.Model.Identity
{
    public class CoreClient<TKey, TRefreshToken>
        where TRefreshToken : CoreRefreshToken<TKey>
    {
        #region Property

        [Key]
        public virtual TKey Id { get; set; }

        public virtual string Secret { get; set; }

        [MaxLength(256)]
        public virtual string Name { get; set; }

        public virtual bool Active { get; set; }

        [MaxLength(256)]
        public virtual string ApplicationType { get; set; }

        public virtual int RefreshTokenLifeTime { get; set; }

        [MaxLength(256)]
        public virtual string AllowedOrigin { get; set; }

        #endregion Property

        #region Collections

        public virtual ICollection<TRefreshToken> RefreshTokens { get; set; }

        #endregion Collections

    }
}
