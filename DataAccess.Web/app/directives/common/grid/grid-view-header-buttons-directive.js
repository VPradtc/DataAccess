'use strict'
angular.module('DataAccessApp').directive('ixGridViewHeaderButtons', function () {
    return {
        restrict: 'EA',
        scope: {
            buttons: '<',
        },
        templateUrl: 'app/views/common/grid/grid-view-header-buttons.html',
        controller: ['$scope', '$state', GridViewHeaderButtonsController],
    };
});
function GridViewHeaderButtonsController($scope, $state) {

    var vm = $scope;

    var _createNavigationCommand = function (state) {
        return function () {
            $state.go(state);
        }
    }

    var _createDecoratedButton = function (button) {
        var copy = angular.copy(button);

        if (copy.action === undefined) {
            copy.action = _createNavigationCommand(copy.state);
        }

        return copy;
    }

    var _decorateButtons = function () {

        if (vm.buttons === undefined) {
            return;
        }

        vm.decoratedButtons = vm.buttons.map(_createDecoratedButton);
    }

    vm.$watch('buttons', _decorateButtons);
}
