'use strict'
angular.module('DataAccessApp').controller('gridViewController',
[
    '$controller',
    '$scope', GridViewController]);

function GridViewController($controller, $scope) {

    var ctrl = this;
    var vm = $scope;

    var _init = function () {

        if (vm.config === undefined) {
            return;
        }

        var gridConfig = angular.copy(vm.config);
        delete gridConfig.view;
        vm.gridConfig = gridConfig;

        var viewConfig = angular.copy(vm.config);
        delete viewConfig.grid;
        vm.options = viewConfig;
    }

    vm.$watch('config', _init);
}
