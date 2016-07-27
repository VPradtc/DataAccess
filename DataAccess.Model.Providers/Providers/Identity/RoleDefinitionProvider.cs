using DataAccess.Entities.Enums;
using DataAccess.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Model.Definition.Providers.Identity
{
    public sealed class RoleDefinitionProvider : IRoleDefinitionProvider
    {
        private List<RoleDefinition> _roleDefinitions = new List<RoleDefinition>
        {
            new RoleDefinition
            {
                Identifier = RoleIdentifier.Admin,
                Name = "Admin"
            },
            new RoleDefinition
            {
                Identifier = RoleIdentifier.Manager,
                Name = "Manager"
            },
            new RoleDefinition
            {
                Identifier = RoleIdentifier.Client,
                Name = "Client"
            },
            new RoleDefinition
            {
                Identifier = RoleIdentifier.ClientRetail,
                Name = "ClientRetail"
            },
        };

        private RoleDefinition Get(Func<RoleDefinition, bool> predicate)
        {
            return _roleDefinitions.First(predicate);
        }

        private RoleDefinition Get<TField>(TField fieldValue, Func<RoleDefinition, TField> fieldSelector)
        {
            return Get((roleDefinition => EqualityComparer<TField>.Default.Equals(fieldSelector(roleDefinition), fieldValue)));
        }

        public RoleDefinition Get(RoleIdentifier identifier)
        {
            return Get(identifier, rd => rd.Identifier);
        }

        public RoleDefinition Get(string name)
        {
            return Get(name, rd => rd.Name);
        }

        public IEnumerable<RoleDefinition> GetAll()
        {
            return _roleDefinitions;
        }
    }
}
