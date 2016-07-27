using DataAccess.Core.Model.Abstraction.Interfaces;
using System;

namespace DataAccess.Dto.Model.Models.Base
{
    public abstract class TrackableDto<T> : TimeTrackableDto, IStatable, ITrackable<T>
    {
        public T CreatedBy { get; set; }
        public T ModifiedBy { get; set; }
    }
}
