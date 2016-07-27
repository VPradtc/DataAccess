'use strict'
angular.module('DataAccessApp').directive('ixSelectEditControl', function () {
    return {
        restrict: 'EA',
        scope: {
            config: '<',
            model: '=ngModel',
            form: '<',
        },
        templateUrl: 'app/views/common/controls/select/select-edit-control.html',
        controller: 'selectEditControlController',
    };
});
