'use strict'
angular.module('DataAccessApp').directive('ixAppTopMenuContent', function () {
    return {
        restrict: 'EA',
        scope: {
        },
        templateUrl: 'app/views/topbar/content/app-top-menu-content.html',
        controller: ['$scope', '$state', 'authService', 'roleDefinitionProvider', AppTopMenuContentController],
    };
});

function AppTopMenuContentController($scope, $state, authService, roleDefinitionProvider) {

    var vm = $scope;

    vm.getFirstName = function () {
        return authService.authentication.firstName;
    }

    vm.getLastName = function () {
        return authService.authentication.lastName;
    }

    vm.getRoleName = function () {

        var roleIdentifier = authService.authentication.role[0];

        return roleDefinitionProvider.getByIdentifier(roleIdentifier).name;
    }

    vm.logout = function () {
        authService.logout().success(function (result) {
            $state.go('app.auth.login');
        });
    };
}
