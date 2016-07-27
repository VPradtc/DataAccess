using DataAccess.Core.Model.Abstraction.Interfaces;
using System;

namespace DataAccess.Dto.Model.Models.Base
{
    public abstract class TimeTrackableDto : StatableDto, IStatable, ITimeTrackable
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
