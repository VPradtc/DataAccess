'use strict'
angular.module('DataAccessApp').directive('ixEditControlBody', function () {
    return {
        restrict: 'EA',
        scope: {
            externalConfig: '<config',
            form: '<',
            model: '=ngModel'
        },
        templateUrl: 'app/views/common/controls/core/edit-control-body.html',
        controller: ['$scope', 'editControlLocator', EditControlBodyController],
    };
});

function EditControlBodyController($scope, editControlLocator) {

    var vm = $scope;

    vm.getControlComponent = function () {

        var componentType = vm.config.attrs.type;

        var result = editControlLocator.getControlComponent(componentType);

        return result;
    }

    var _init = function () {

        if (vm.externalConfig === undefined) {
            return;
        }

        vm.config = angular.copy(vm.externalConfig);
        vm.config.attrs.ngModel = "model[config.field]";
        vm.config.attrs.ixValidator = true;
    }

    vm.$watch('externalConfig', _init);

    _init();
}