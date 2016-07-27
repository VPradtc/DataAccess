'use strict'
angular.module('DataAccessApp').directive('ixPlainDialog', function () {
    return {
        restrict: 'EA',
        scope: {
            externalData: '<data',
        },
        transclude: {
            dialogButtons: '?dialogButtons',
            dialogFooter: '?dialogFooter',
        },
        templateUrl: 'app/views/dialogs/plainDialog.html',
        controller: ['$scope', PlainDialogController]
    };
});

function PlainDialogController($scope) {

    var vm = $scope;

    var _defaults = {
        labels: {
            footer: 'auth.postRegistrationMessageFooter',
            footerEmail: 'auth.postRegistrationMessageFooterEmail',
        }
    }

    var _init = function () {

        if (vm.externalData === undefined) {
            return;
        }

        vm.data = angular.merge({}, _defaults, vm.externalData);
    }

    vm.$watch('externalData', _init);
}