using System;
using DataAccess.Core.Model.Abstraction.Interfaces;
using DataAccess.Entities.Identity;
using System.Threading;
using Microsoft.AspNet.Identity;

namespace DataAccess.Entities.Entities.BaseEntities
{
    public class TrackableEntity : TimeTrackableEntity, ITrackable<Guid>
    {
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }

        public virtual User CreatedByUser { get; set; }
        public virtual User ModifiedByUser { get; set; }

        public TrackableEntity()
            : base()
        {
            Guid userId = Guid.Empty;
            Guid.TryParse(Thread.CurrentPrincipal.Identity.GetUserId(), out userId);

            CreatedBy = userId;
            ModifiedBy = userId;
        }
    }
}
