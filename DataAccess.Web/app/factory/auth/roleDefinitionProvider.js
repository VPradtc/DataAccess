'use strict'
angular.module('DataAccessApp').factory('roleDefinitionProvider', ['enumRoleIdentifier', '$filter', RoleDefinitionProvider]);

function RoleDefinitionProvider(enumRoleIdentifier, $filter) {

    var _roles = [
        {
            identifier: enumRoleIdentifier.admin,
            name: 'auth.role_admin',
        },
        {
            identifier: enumRoleIdentifier.manager,
            name: 'auth.role_manager',
        },
        {
            identifier: enumRoleIdentifier.client,
            name: 'auth.role_client',
        },
        {
            identifier: enumRoleIdentifier.clientRetail,
            name: 'auth.role_clientRetail',
        },
    ];

    return {
        getAll: function () {
            return _roles;
        },
        getByIdentifier: function (identifier) {
            return $filter('filter')(_roles, { identifier: identifier })[0];
        }
    };
}
