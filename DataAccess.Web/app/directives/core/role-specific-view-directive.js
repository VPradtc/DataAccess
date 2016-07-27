'use strict'
angular.module('DataAccessApp').directive('ixRoleSpecificView', function () {
    return {
        restrict: 'EA',
        scope: {
            route: '@?'
        },
        templateUrl: 'app/views/core/role-specific-view.html',
        controller: [
            '$scope',
            '$state',
            'authService',
            'roleSpecificViewLocator',
            RoleSpecificViewController
        ],
    };
});

function RoleSpecificViewController(
    $scope,
    $state,
    authService,
    roleSpecificViewLocator) {

    var vm = $scope;

    var _state = vm.route || $state.current.name;
    var _role = authService.authentication.role[0];

    var _viewComponent = roleSpecificViewLocator.getViewComponent(_state, _role);

    vm.viewAttrs = {};
    vm.viewAttrs[_viewComponent] = true;
}
