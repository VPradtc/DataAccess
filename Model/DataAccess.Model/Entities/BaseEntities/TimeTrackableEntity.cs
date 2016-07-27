using System;
using DataAccess.Core.Model.Abstraction.Interfaces;

namespace DataAccess.Entities.Entities.BaseEntities
{
    public class TimeTrackableEntity : StatableEntity, ITimeTrackable
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public TimeTrackableEntity()
            : base()
        {
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;
        }
    }
}
