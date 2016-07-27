using DataAccess.Entities.Enums;
using DataAccess.Entities.Identity;

namespace DataAccess.Model.Definition.Providers.Identity
{
    public interface IRoleDefinitionProvider : IDefinitionProvider<RoleDefinition>
    {
        RoleDefinition Get(string name);
        RoleDefinition Get(RoleIdentifier identifier);
    }
}
