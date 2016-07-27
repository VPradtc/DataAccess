'use strict'
angular.module('DataAccessApp').directive('ixHeaderButton', function () {
    return {
        restrict: 'EA',
        scope: {
            config: '<',
        },
        templateUrl: 'app/views/common/controls/header-button.html',
        controller: ['$scope', HeaderButtonController],
    };
});

function HeaderButtonController($scope) {

    var ctrl = this;
    var vm = $scope;

    var _init = function () {

        if (vm.config === undefined) {
            return;
        };

        vm.config.class = vm.config.class || 'btn-primary';
    }

    vm.$watch('config', _init);
}
