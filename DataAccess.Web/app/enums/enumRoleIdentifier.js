angular.module('DataAccessApp').factory('enumRoleIdentifier', EnumRoleIdentifier);

function EnumRoleIdentifier() {

    return {
        clientRetail: 1,
        client: 2,
        manager: 3,
        admin: 4,
    };
}
