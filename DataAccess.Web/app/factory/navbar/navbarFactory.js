angular.module('DataAccessApp').factory('navbarFactory',
['enumRoleIdentifier', NavbarFactory]);
function NavbarFactory(enumRoleIdentifier) {

    var _createNavbar = function () {

        var navbar = {
            title: "",
            items: [
                {
                    title: 'common.users',
                    state: 'app.main.user.index',
                    icon: 'fa fa-user',
                    restrict: {
                        roles: [enumRoleIdentifier.admin],
                    },
                }
            ]
        };

        return navbar;
    };

    return {
        createNavbar: _createNavbar
    };
}
