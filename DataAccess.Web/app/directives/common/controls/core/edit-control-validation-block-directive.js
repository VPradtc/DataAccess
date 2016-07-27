'use strict'
angular.module('DataAccessApp').directive('ixEditControlValidationBlock', function () {
    return {
        restrict: 'EA',
        scope: {
            validationConfig: '<',
            form: '<',
        },
        templateUrl: 'app/views/common/controls/core/edit-control-validation-block.html',
        controller: ['$scope', 'validationTriggerFactory', EditControlValidationBlockController],
    };
});

function EditControlValidationBlockController($scope, validationTriggerFactory) {

    var vm = $scope;

    var _init = function () {

        if (vm.validationConfig === undefined) {
            return;
        }

        vm.config = angular.copy(vm.validationConfig);

        vm.config.trigger = validationTriggerFactory.create(vm.validationConfig.trigger, vm.form, vm.validationConfig.name);

        vm.config.messages.forEach(function (message) {
            message.trigger = validationTriggerFactory.create(message.trigger, vm.form, vm.validationConfig.name);
        });
    }

    vm.$watch('validationConfig', _init);
}