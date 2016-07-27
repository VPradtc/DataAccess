using System;

namespace DataAccess.Core.Model.Abstraction.Interfaces
{
    public interface ITimeTrackable
    {
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}