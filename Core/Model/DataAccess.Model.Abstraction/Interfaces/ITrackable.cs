using System;

namespace DataAccess.Core.Model.Abstraction.Interfaces
{
    public interface ITrackable<TKey> : ITimeTrackable
    {
        TKey CreatedBy { get; set; }
        TKey ModifiedBy { get; set; }
    }
}