'use strict'
angular.module('DataAccessApp').directive('ixTab', function () {
    return {
        restrict: 'EA',
        transclude: true,
        scope: {
            tab: '<',
        },
        require: '^^ixTabs',
        templateUrl: 'app/views/common/tabs/tab.html',
        controller: TabController,
        link: function ($scope, element, attrs, tabsCtrl) {

            var vm = $scope;

            vm.isActive = function () {
                return tabsCtrl.isActive(vm.tab);
            }
        }
    };
});

function TabController($scope) {

    var ctrl = this;
    var vm = $scope;
}