'use strict'
angular.module('DataAccessApp').directive('ixInputEditControl', function () {
    return {
        restrict: 'EA',
        scope: {
            config: '<',
            model: '=ngModel',
            form: '<',
        },
        templateUrl: 'app/views/common/controls/input-edit-control.html',
        controller: ['$scope', InputEditControlController],
    };
});

function InputEditControlController($scope) {

    var ctrl = this;
    var vm = $scope;
}
