using DataAccess.Core.Model.Abstraction.Interfaces;

namespace DataAccess.Dto.Model.Models.Base
{
    public abstract class StatableDto : PrimaryDto, IStatable
    {
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
