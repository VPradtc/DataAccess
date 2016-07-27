'use strict'
angular.module('DataAccessApp').directive('ixEditControl', function () {
    return {
        restrict: 'E',
        scope: {
            config: '<',
            form: '<',
            model: '=ngModel'
        },
        templateUrl: 'app/views/common/controls/edit-control.html',
        controller: ['$scope', '$state', 'validationConfigFactory', EditControlController],
    };
});

function EditControlController($scope, $state, validationConfigFactory) {

    var vm = $scope;

    vm.render = function () {

        var isCorrectState = vm.config.mode === undefined
            || vm.config.mode === $state.current.mode;

        var userDefinedRender = vm.config.render !== undefined
        ? vm.config.render(vm.model)
        : true;

        return isCorrectState && userDefinedRender;
    }

    var _init = function () {

        vm.validationConfig = validationConfigFactory.create(vm.config);
    }

    vm.$watch('config', _init);
}