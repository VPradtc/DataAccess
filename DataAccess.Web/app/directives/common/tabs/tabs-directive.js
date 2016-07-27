'use strict'
angular.module('DataAccessApp').directive('ixTabs', function () {
    return {
        restrict: 'EA',
        transclude: true,
        scope: {
            tabs: '<',
        },
        templateUrl: 'app/views/common/tabs/tabs.html',
        controller: TabsController,
    };
});

function TabsController($scope) {

    var ctrl = this;
    var vm = $scope;

    vm.setActive = function (tab) {
        vm.activeTab = tab;
    }

    vm.isActive = function (tab) {
        return vm.activeTab === tab;
    }

    ctrl.isActive = vm.isActive;

    var _init = function () {

        if (vm.tabs === undefined
            || (vm.activeTab !== undefined
                && vm.tabs.includes(vm.activeTab))) {
            return;
        }

        vm.setActive(vm.tabs[0]);
    }

    vm.$watch('tabs', _init);
}