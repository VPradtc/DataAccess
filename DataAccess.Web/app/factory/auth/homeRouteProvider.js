'use strict'
angular.module('DataAccessApp').factory('homeRouteProvider',
    ['enumRoleIdentifier', 'authService', HomeRouteProvider]);
function HomeRouteProvider(enumRoleIdentifier, authService) {

    var _routes = {
        default: 'app.auth.login'
    };
    _routes[enumRoleIdentifier.admin] = 'app.main.user.index'

    var _getRouteFor = function (roleIdentifier) {
        return routes[roleIdentifier] || _routes.default;
    };

    var _getRoute = function() {
        var authData = authService.loadAuthenticationData();

        if (authData === undefined
            || !Array.isArray(authData.role)) {
            return _routes.default;
        }

        var userRole = authData.role.find(function (roleIdentifier) {
            return _routes[roleIdentifier] !== undefined;
        });

        return _routes[userRole];
    }

    return {
        getRouteFor: _getRouteFor,
        getRoute: _getRoute,
    }
}