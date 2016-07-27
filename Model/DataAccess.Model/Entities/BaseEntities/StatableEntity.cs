using System;
using DataAccess.Core.Model.Abstraction.Interfaces;

namespace DataAccess.Entities.Entities.BaseEntities
{
    public class StatableEntity : PrimaryEntity, IStatableEntity
    {
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public StatableEntity()
        {
            IsActive = true;
        }

        public void Activate()
        {
            IsActive = true;
            IsDeleted = false;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}
