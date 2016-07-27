'use strict'
angular.module('DataAccessApp').directive('ixNavbarItem', function () {
    return {
        restrict: 'EA',
        scope: {
            item: '=',
        },
        replace: true,
        templateUrl: 'app/views/navbar/navbar-item.html',
        controller: ['$state', '$scope', NavbarItemController],
    };
});

function NavbarItemController($state, $scope) {

    var vm = $scope;

    vm.getClasses = function () {
        return {
            active: $state.is(vm.item.state),
        };
    }
}
