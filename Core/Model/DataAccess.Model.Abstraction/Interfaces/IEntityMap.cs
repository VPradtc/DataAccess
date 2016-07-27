using System.Data.Entity;

namespace DataAccess.Core.Model.Abstraction.Interfaces
{
    public interface IEntityMap
    {
        #region Methods

        void Map(DbModelBuilder modelBuilder);

        #endregion
    }
}
