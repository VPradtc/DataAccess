'use strict'
angular.module('DataAccessApp').directive('ixComboboxEditControl', function () {
    return {
        restrict: 'EA',
        scope: {
            config: '<',
            model: '=ngModel',
            form: '<',
        },
        templateUrl: 'app/views/common/controls/select/combobox-edit-control.html',
        controller: 'selectEditControlController',
    };
});
