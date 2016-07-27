'use strict'
angular.module('DataAccessApp').directive('ixNavbar', function () {
    return {
        restrict: 'EA',
        scope: {
            item: '=',
        },
        templateUrl: 'app/views/navbar/navbar.html',
        controller: ['navbarFactory', 'authService', '$scope', NavbarController],
    };
});

function NavbarController(navbarFactory, authService, $scope) {

    var vm = $scope;

    vm.showItem = function (item) {

        if (!item.restrict) {
            return true;
        }

        if (!item.restrict.roles) {
            return true;
        }

        return authService.checkRole(item.restrict.roles);
    }

    vm.navbar = navbarFactory.createNavbar();
}
