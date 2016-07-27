'use strict'
angular.module('DataAccessApp').directive('ixDialog', function () {
    return {
        restrict: 'EA',
        scope: {
            title: '<',
            text: '<',
        },
        transclude: {
            dialogIcon: 'dialogIcon',
            dialogButtons: 'dialogButtons',
        },
        templateUrl: 'app/views/dialogs/dialog.html',
    };
});