'use strict'
angular.module('DataAccessApp').factory('userModel', ['roleDefinitionProvider', UserModel]);

function UserModel(roleDefinitionProvider) {
    return kendo.data.Model.define({
        id: 'id',
        fields: {
            firstName: { type: 'string' },
            lastName: { type: 'string' },
            email: { type: 'string' },
            phoneNumber: { type: 'string' },
            roleId: { type: 'number' },
        },
        role: function () {
            return roleDefinitionProvider.getByIdentifier(this.roleId).name;
        },
    });
}
