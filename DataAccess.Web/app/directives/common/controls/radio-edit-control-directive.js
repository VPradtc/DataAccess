'use strict'
angular.module('DataAccessApp').directive('ixRadioEditControl', function () {
    return {
        restrict: 'EA',
        scope: {
            externalConfig: '<config',
            model: '=ngModel',
            form: '<',
        },
        templateUrl: 'app/views/common/controls/radio-edit-control.html',
        controller: ['$scope', RadioEditControlController],
    };
});

function RadioEditControlController($scope) {

    var ctrl = this;
    var vm = $scope;

    var _mapRadioItem = function (item) {

        var result = {
            label: item.label,
        };
        var resultAttrs = {
            ngValue: item.value,
        }

        result.attrs = angular.merge({}, vm.externalConfig.attrs, resultAttrs);

        return result;
    }

    var _init = function () {

        var config = angular.copy(vm.externalConfig);
        config.options = vm.externalConfig.options.map(_mapRadioItem);

        vm.config = config;
    }

    vm.$watch('externalConfig', _init);
}
