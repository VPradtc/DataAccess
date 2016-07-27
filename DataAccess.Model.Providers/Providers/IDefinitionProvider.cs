using System.Collections.Generic;

namespace DataAccess.Model.Definition.Providers
{
    public interface IDefinitionProvider<TDefinition>
    {
        IEnumerable<TDefinition> GetAll();
    }
}
