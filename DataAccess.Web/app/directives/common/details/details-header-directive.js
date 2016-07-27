'use strict'
angular.module('DataAccessApp').directive('ixDetailsHeader', function () {
    return {
        restrict: 'EA',
        scope: {
            config: '<',
        },
        templateUrl: 'app/views/common/details/details-header.html',
        controller: ['$scope', DetailsHeaderController],
    };
});

function DetailsHeaderController($scope) {

    var vm = $scope;

    var _viewMap = {
        'edit': {
            breadcrumb: [
                {
                    title: 'common.edit',
                }],
            buttons: {
                confirm: {
                    text: 'common.save'
                },
                cancel: {
                    text: 'common.cancel'
                },
            }
        },
        'create': {
            breadcrumb: [
                {
                    title: 'common.create',
                }],
            buttons: {
                confirm: {
                    text: 'common.create'
                },
                cancel: {
                    text: 'common.cancel'
                },
            }
        }
    };

    var _getViewTemplate = function (mode) {
        return _viewMap[mode];
    };

    var _init = function () {

        if (vm.config === undefined) {
            return;
        }

        var mode = vm.config.mode;

        var viewTemplate = _getViewTemplate(mode);

        vm.options = angular.merge({}, viewTemplate, vm.config);
        vm.options.breadcrumb = vm.config.breadcrumb.concat(viewTemplate.breadcrumb);
    }

    vm.$watch('config', _init);
}
