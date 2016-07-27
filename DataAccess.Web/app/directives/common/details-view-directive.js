'use strict'
angular.module('DataAccessApp').directive('ixDetailsView', function () {
    return {
        restrict: 'EA',
        scope: {
            config: '<',
        },
        templateUrl: 'app/views/common/details-view.html',
        controller: ['$controller', '$scope', '$state', DetailsController],
    };
});

function DetailsController($controller, $scope, $state) {

    var ctrl = this;
    var vm = $scope;

    vm.model = {
        entity: {},
        form: {},
    };

    var _viewMap = {
        'edit': {
            breadcrumb: [
                {
                    title: 'common.edit',
                }],
            labels: {
                confirm: 'common.save',
            }
        },
        'create': {
            breadcrumb: [
                {
                    title: 'common.create',
                }],
            labels: {
                confirm: 'common.create',
            }
        }
    };

    var _getViewTemplate = function () {
        return _viewMap[vm.options.mode];
    };

    var _init = function () {

        if (vm.config === undefined) {
            return;
        }

        vm.options = angular.copy(vm.config);
        vm.options.entity = vm.model.entity;

        vm.options.mode = vm.options.mode || $state.current.mode;

        var viewTemplate = _getViewTemplate();
        vm.options.view.labels = angular.merge(vm.options.view.labels, viewTemplate.labels);
        vm.options.view.breadcrumb = vm.options.view.breadcrumb.concat(viewTemplate.breadcrumb);

        angular.extend(ctrl, $controller('baseEditController', { $scope: vm }));

        vm.validateAndSave = function () {
            if (vm.model.form.$valid) {
                vm.save();
            } else {
                vm.model.form.submitted = true;
            }
        }
    }

    $scope.$watch('config', _init);
}
