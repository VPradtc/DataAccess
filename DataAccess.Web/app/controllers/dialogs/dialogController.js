'use strict';
angular.module('DataAccessApp').controller('dialogController',
['$scope', DialogController]);

function DialogController($scope) {

    var vm = $scope;

    vm.data = vm.ngDialogData;
}
