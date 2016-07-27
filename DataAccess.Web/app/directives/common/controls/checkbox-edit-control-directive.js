'use strict'
angular.module('DataAccessApp').directive('ixCheckboxEditControl', function () {
    return {
        restrict: 'EA',
        scope: {
            config: '<',
            model: '=ngModel',
            form: '<',
        },
        templateUrl: 'app/views/common/controls/checkbox-edit-control.html',
        controller: ['$scope', CheckboxEditControlController],
    };
});

function CheckboxEditControlController($scope) {

    var ctrl = this;
    var vm = $scope;
}
