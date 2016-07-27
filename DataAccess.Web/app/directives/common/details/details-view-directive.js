'use strict'
angular.module('DataAccessApp').directive('ixDetailsView', function () {
    return {
        restrict: 'EA',
        scope: {
            config: '<',
        },
        templateUrl: 'app/views/common/details/details-view.html',
        controller: ['$scope', '$state', 'detailsStrategyFactory', DetailsController],
    };
});

function DetailsController(
    $scope,
    $state,
    detailsStrategyFactory) {

    var ctrl = this;
    var vm = $scope;

    var _strategy = {};

    vm.model = {
        entity: {},
        form: {},
    };

    vm.save = function () {
        return _strategy.save();
    }

    vm.cancel = function () {
        return _strategy.cancel();
    }

    vm.init = function () {
        return _strategy.init();
    }

    vm.validateAndSave = function () {
        if (vm.model.form.$valid) {
            vm.save();
        } else {
            vm.model.form.submitted = true;
        }
    }

    var _viewTemplate = {
        buttons: {
            confirm: {
                action: vm.validateAndSave,
            },
            cancel: {
                action: function () {
                    return vm.cancel();
                }
            },
        }
    };

    var _getCurrentMode = function () {
        return vm.options.mode || $state.current.mode;
    }

    var _init = function () {

        if (vm.config === undefined) {
            return;
        }

        vm.options = angular.copy(vm.config);
        vm.options.entity = vm.model.entity;

        vm.headerConfig = angular.merge({ mode: _getCurrentMode(), }, _viewTemplate, vm.options.view);

        vm.options.mode = _getCurrentMode();

        _strategy = detailsStrategyFactory.create(vm.options);
        vm.init();
    }

    $scope.$watch('config', _init);
}
